using Sitecore.Data.Fields;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using Sitecore.Plugins.Alaska.Contents.Adapters.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Adapters
{
    public class FieldAdaptersCollection
    {
        private static readonly FieldAdaptersCollection _Instance = new FieldAdaptersCollection();
        public static FieldAdaptersCollection Current => _Instance;

        private Dictionary<string, IFieldAdapter> _adapters = new Dictionary<string, IFieldAdapter>();
        private IFieldAdapter _defaultAdapter;

        private FieldAdaptersCollection()
        { }

        public void Add(IDictionary<string, IFieldAdapter> adapters)
        {
            adapters.ToList().ForEach(x => Add(x.Key.ToLower(), x.Value));
        }

        public void Add<T>(string fieldType, FieldAdapter<T> adapter)
            where T : class
        {
            Add(fieldType, adapter);
        }

        public void Add(string fieldType, IFieldAdapter adapter)
        {
            if (_adapters.ContainsKey(fieldType))
                throw new InvalidOperationException($"Field type {fieldType} already registered");
            _adapters.Add(fieldType, adapter);
        }

        public void Remove(string fieldType)
        {
            _adapters.Remove(fieldType);
        }

        public void SetDefaultFieldAdapter(IFieldAdapter adapter)
        {
            _defaultAdapter = adapter;
        }

        public IFieldAdapter GetAdapter(string fieldType)
        {
            return _adapters.ContainsKey(fieldType.ToLower()) ?
                _adapters[fieldType.ToLower()] :
                _defaultAdapter;
        }
    }
}
