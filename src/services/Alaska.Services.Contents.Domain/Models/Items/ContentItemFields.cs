using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Items
{
    public class ContentItemFields : Dictionary<string, ContentItemField>
    {
        public ContentItemFields()
        { }

        public ContentItemFields(IDictionary<string, ContentItemField> fields)
        {
            fields.ToList().ForEach(x => Add(x.Key, x.Value));
        }

        public ContentItemField GetField(string fieldId) => ContainsKey(fieldId) ? this[fieldId] : null;
    }
}
