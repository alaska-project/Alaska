using Alaska.Common.Caching.Exceptions;
using Alaska.Common.Caching.Abstractions;
using Alaska.Common.Caching.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching
{
    public abstract class CacheInstance : ICacheInstance
    {
        private const int DefaultExpirationMinutes = 60;
        private const bool DefaultCacheNullItems = true;

        private ICacheOptions _cacheSettings;
        private ICacheEngine _cache;

        private ConcurrentDictionary<string, CacheEntryLockItem> _locks = new ConcurrentDictionary<string, CacheEntryLockItem>();

        protected ICacheEngine Cache => _cache;
        protected TimeSpan DefaultExpirationTime => _cacheSettings != null ?
            _cacheSettings.DefaultExpiration :
            TimeSpan.FromMinutes(DefaultExpirationMinutes);

        protected CacheExpirationMode ExpirationMode => _cacheSettings.CacheExpirationMode;

        protected bool CacheNullItems => _cacheSettings.CacheNullItems;

        public CacheInstance()
        { }

        internal void SetCacheOptions(ICacheOptions cacheSettings)
        {
            _cacheSettings = cacheSettings;
        }

        internal void SetCacheEngine(ICacheEngine cacheEngine)
        {
            _cache = cacheEngine;
        }

        public virtual T Retreive<T>(string key, Func<T> initializationExpression)
        {
            return Retreive(key, initializationExpression, DefaultExpirationTime);
        }

        public virtual T Retreive<T>(string key, Func<T> initializationExpression, TimeSpan expiration)
        {
            return Retreive(key, initializationExpression, expiration, DefaultCacheNullItems);
        }

        public virtual T Retreive<T>(string key, Func<T> initializationExpression, TimeSpan expiration, bool cacheNullItems)
        {
            T value;
            if (GetValueAndScheduleUpdate<T>(key, initializationExpression, expiration, out value))
                return value;

            //ottengo un oggetto su cui fare lock differente in base alla key di modo che l'inizializzazione di oggetti su key diverse possa avvenire in maniera concorrente
            var lockObj = GetLockObject(key);
            lock (lockObj)
            {
                //controllo se nel tempo di wait del lock qualche altro thread abbia gia inizializzato il valore in cache
                if (Get<T>(key, out value))
                    return value;

                //inizializzo il valore e lo scrivo in cache
                lockObj.State = CacheEntryState.Loading;

                value = InitializeCacheEntry(key, initializationExpression, expiration, cacheNullItems);

                lockObj.State = CacheEntryState.Ready;
            }

            return value;
        }

        public virtual bool Get<T>(string key, out T value)
        {
            try
            {
                var item = GetItem<T>(key);
                if (item == null)
                {
                    value = default(T);
                    return false;
                }

                value = item.Value;
                return true;
            }
            catch (CacheServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error searching key {0} in cache {1}", key, CacheId), e);
            }
        }

        public virtual void Set<T>(string key, T value)
        {
            Set(key, value, DefaultExpirationTime);
        }

        public virtual void Set<T>(string key, T value, TimeSpan expiration)
        {
            try
            {
                if (IsDisabled || (value == null && !CacheNullItems))
                    return;

                var expirationDate = expiration == TimeSpan.MaxValue ?
                    DateTime.MaxValue :
                    DateTime.Now.Add(expiration);

                var item = new CacheItem<T>
                {
                    Key = key,
                    Value = value,
                    Expiration = expiration,
                    ExpirationTime = expirationDate,
                };
                _cache.Set<T>(item);
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error setting key {0} in cache {1}", key, CacheId), e);
            }
        }

        public virtual void Remove(string key)
        {
            try
            {
                if (IsDisabled)
                    return;

                _cache.Remove(key);
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error removing key {0} in cache {1}", key, CacheId), e);
            }
        }

        public virtual void Clear()
        {
            try
            {
                _cache.Clear();
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error clearing cache {0}", CacheId), e);
            }
        }

        public virtual IEnumerable<string> Keys
        {
            get
            {
                try
                {
                    return _cache.Keys;
                }
                catch (Exception e)
                {
                    throw new CacheServiceException(string.Format("Error getting all keys of cache {0}", CacheId), e);
                }
            }
        }

        public virtual ICacheItem<T> GetItem<T>(string key)
        {
            try
            {
                return IsDisabled ? null : _cache.Get<T>(key);
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error searching item key {0} in cache {1}", key, CacheId), e);
            }
        }

        public virtual ICacheItem GetItem(string key)
        {
            try
            {
                return IsDisabled ? null : _cache.Get(key);
            }
            catch (Exception e)
            {
                throw new CacheServiceException(string.Format("Error searching item key {0} in cache {1}", key, CacheId), e);
            }
        }

        public string CacheId => GetType().FullName;

        public string CacheEngine => _cache.GetType().Name;

        public bool IsDisabled => _cacheSettings.IsDisabled;

        private CacheEntryLockItem GetLockObject(string key)
        {
            return _locks.GetOrAdd(key, x => new CacheEntryLockItem(CacheEntryState.Uninitialized));
        }

        private bool GetValueAndScheduleUpdate<T>(string key, Func<T> initializationExpression, TimeSpan expiration, out T value)
        {
            var cacheItem = GetItem<T>(key);
            if (cacheItem == null)
            {
                value = default(T);
                return false;
            }

            // se la modalità di expiration per questa cache è Logical controllo il LogicalExpirationTime 
            if (ExpirationMode == CacheExpirationMode.Logical)
            {
                var isExpired = DateTime.Now > cacheItem.LogicalExpirationTime;
                if (isExpired)
                    InitializeCacheEntryAsync(key, initializationExpression, expiration);
            }

            value = cacheItem.Value;
            return true;
        }

        private T InitializeCacheEntry<T>(string key, Func<T> initializationExpression, TimeSpan expiration, bool cacheNullItems)
        {
            //inizializzo il valore e lo scrivo in cache
            T value;

            if (ExpirationMode == CacheExpirationMode.Physical)
            {
                value = initializationExpression();
            }
            //se la modalità di expiration è Logical invoco comunque l'inizializzazione della entry in un thread separato
            //in modo che eventuali problemi di Context null emergano già al primo caricamento della entry in cache e non solo alla prima scadenza logica
            else
            {
                var task = Task.Run(() => initializationExpression());
                task.Wait();
                value = task.Result;
            }

            if (value != null || cacheNullItems)
                Set(key, value, expiration);

            return value;
        }

        private void InitializeCacheEntryAsync<T>(string key, Func<T> initializationExpression, TimeSpan expiration)
        {
            var lockObj = GetLockObject(key);
            lock (lockObj)
            {
                if (lockObj.State == CacheEntryState.Loading)
                    return;

                lockObj.State = CacheEntryState.Loading;
            }

            // rilascio il lock e lancio il thread che andrà a svolgere l'inizializzazione
            Task.Run(() => InvokeEntryInitializazionExpression<T>(key, lockObj, initializationExpression, expiration));
        }

        private void InvokeEntryInitializazionExpression<T>(string key, CacheEntryLockItem lockItem, Func<T> initializationExpression, TimeSpan expiration)
        {
            try
            {
                var value = initializationExpression();
                Set<T>(key, value, expiration);
            }
            catch (Exception e)
            {
                throw new CacheInitializationExeption($"Error initializing cache {CacheId} entry {key}", e);
            }
            finally
            {
                lockItem.State = CacheEntryState.Ready;
            }
        }
    }
}
