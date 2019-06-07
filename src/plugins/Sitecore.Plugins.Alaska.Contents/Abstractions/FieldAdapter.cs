using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;

namespace Sitecore.Plugins.Alaska.Contents.Abstractions
{
    public abstract class FieldAdapter<TField> : IFieldAdapter
    {
        public Type FieldType => typeof(TField);
        public abstract TField GetField(Field field);

        public abstract ContentItemField AdaptField(Field field);
        public abstract void UpdateField(ContentItemField value, Field field);
    }
}
