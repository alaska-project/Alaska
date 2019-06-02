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

        private Dictionary<Type, IFieldAdapter> _adapters = new Dictionary<Type, IFieldAdapter>();
        private IFieldAdapter _defaultAdapter;

        private FieldAdaptersCollection()
        { }

        public void Add<T>(FieldAdapter<T> adapter)
            where T : class
        {
            Add(adapter.FieldType, adapter);
        }

        public void Add(Type fieldType, IFieldAdapter adapter)
        {
            if (_adapters.ContainsKey(fieldType))
                throw new InvalidOperationException($"Field type {fieldType.FullName} already registered");
            _adapters.Add(fieldType, adapter);
        }

        public void Remove(Type fieldType)
        {
            _adapters.Remove(fieldType);
        }

        public void SetDefaultFieldAdapter(IFieldAdapter adapter)
        {
            _defaultAdapter = adapter;
        }

        public IFieldAdapter GetAdapter(Type fieldType)
        {
            return _adapters.ContainsKey(fieldType) ?
                _adapters[fieldType] :
                _defaultAdapter;
        }
    }
}
