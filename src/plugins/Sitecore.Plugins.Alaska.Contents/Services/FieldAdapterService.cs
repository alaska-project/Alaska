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
        public ContentItemField AdaptField(Field field)
        {
            var fieldAdapter = FieldAdaptersCollection.Current.GetAdapter(GetFieldType(field));
            return fieldAdapter.AdaptField(field);
        }

        public void UpdateField(ContentItemField value, Field field)
        {
            var fieldAdapter = FieldAdaptersCollection.Current.GetAdapter(GetFieldType(field));
            fieldAdapter.UpdateField(value, field);
        }

        private string GetFieldType(Field field) => field.TypeKey;
    }
}
