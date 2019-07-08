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
        private const bool DefaultCacheNullItems = true;

        private ICacheOptions _cacheSettings;
        private ICacheEngine _cache;

        private ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();

        protected ICacheEngine Cache => _cache;
        protected TimeSpan DefaultExpirationTime => _cacheSettings.DefaultExpiration;
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
            if (Get<T>(key, out T value))
                return value;
            
            //inizializzo il valore e lo scrivo in cache
            value = initializationExpression();
            Set(key, value, expiration);

            return value;
        }

        public virtual T RetreiveExclusive<T>(string key, Func<T> initializationExpression)
        {
            return RetreiveExclusive(key, initializationExpression, DefaultExpirationTime);
        }

        public virtual T RetreiveExclusive<T>(string key, Func<T> initializationExpression, TimeSpan expiration)
        {
            if (Get<T>(key, out T value))
                return value;

            //ottengo un oggetto su cui fare lock differente in base alla key di modo che l'inizializzazione di oggetti su key diverse possa avvenire in maniera concorrente
            var lockObj = _locks.GetOrAdd(key, x => new object());
            lock (lockObj)
            {
                //controllo se nel tempo di wait del lock qualche altro thread abbia gia inizializzato il valore in cache
                if (Get<T>(key, out value))
                    return value;

                //inizializzo il valore e lo scrivo in cache
                value = initializationExpression();
                Set(key, value, expiration);
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
    }
}
