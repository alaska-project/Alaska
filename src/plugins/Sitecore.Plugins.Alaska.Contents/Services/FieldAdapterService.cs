using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using Sitecore.Plugins.Alaska.Contents.Adapters;
using Sitecore.Plugins.Alaska.Contents.Adapters.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Services
{
    public class FieldAdapterService
    {
        private static IDictionary<string, IFieldAdapter> _DefaultAdapters = new Dictionary<string, IFieldAdapter>
        {
            { "Checkbox", new CheckboxFieldAdapter() },
            { "Date", new DateTimeFieldAdapter() },
            { "Datetime", new DateTimeFieldAdapter() },
            { "Image", new ImageFieldAdapter() },
            { "Number", new DecimalFieldAdapter() },
            { "Integer", new IntFieldAdapter() },
            { "General Link", new LinkFieldAdapter() },
        };

        static FieldAdapterService()
        {
            FieldAdaptersCollection.Current.SetDefaultFieldAdapter(new DefaultFieldAdapter());
            FieldAdaptersCollection.Current.Add(_DefaultAdapters);
        }

        public ContentItemField AdaptField(Field field)
        {
            var fieldAdapter = FieldAdaptersCollection.Current.GetAdapter(GetFieldType(field));
            return fieldAdapter.AdaptField(field);
        }

        private string GetFieldType(Field field) => field.TypeKey;
    }
}
