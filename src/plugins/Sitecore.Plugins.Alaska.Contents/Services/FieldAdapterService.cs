using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
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
        static FieldAdapterService()
        {
            FieldAdaptersCollection.Current.SetDefaultFieldAdapter(new DefaultFieldAdapter());
            FieldAdaptersCollection.Current.Add(new CheckboxFieldAdapter());
            FieldAdaptersCollection.Current.Add(new LinkFieldAdapter());
            FieldAdaptersCollection.Current.Add(new ImageFieldAdapter());
        }

        public ContentItemField AdaptField(Field field)
        {
            var fieldAdapter = FieldAdaptersCollection.Current.GetAdapter(field.GetType());
            return fieldAdapter.AdaptField(field);
        }
    }
}
