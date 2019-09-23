using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Extensions.Contents.Contentful.Fields;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Converters
{
    public class FieldAdaptersCollection
    {
        private Dictionary<string, IFieldAdapter> _adapters;

        public FieldAdaptersCollection(ContentfulClientsFactory clientsFactory)
        {
            _adapters = new Dictionary<string, IFieldAdapter>()
            {
                { "Boolean", new BooleanFieldAdapter() },
                { "Integer", new IntegerFieldAdapter() },
                { "Location", new CoordinatesFieldAdapter() },
                { "Date", new DateTimeFieldAdapter() },
                { "Object", new JsonFieldAdapter() },
                { "Link", new LinkFieldAdapter(clientsFactory) },
                { "RichText", new RichTextFieldAdapter() },
                { "Text", new RichTextFieldAdapter() },
                { "Symbol", new StringFieldAdapter() },
                { "Array", new StringListFieldAdapter() },
            };
        }

        public void RegisterAdapter(string fieldType, IFieldAdapter adapter) => _adapters.Add(fieldType, adapter);

        public IFieldAdapter ResolveAdapter(string fieldType)
        {
            if (!_adapters.ContainsKey(fieldType))
                throw new InvalidOperationException($"Cannot resolve adapter for fieldType {fieldType}");

            return _adapters[fieldType];
        }
    }
}
