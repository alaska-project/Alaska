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
        where TField : class
    {
        public Type FieldType => typeof(TField);
        public TField GetField(Field field) => field as TField;

        public abstract ContentItemField AdaptField(Field field);
    }
}
