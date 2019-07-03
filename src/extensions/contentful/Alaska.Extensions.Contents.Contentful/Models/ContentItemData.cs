using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Models
{
    public class ContentItemData : Dictionary<string, dynamic>
    {
        public dynamic GetField(string fieldId) => ContainsKey(fieldId) ? this[fieldId] : null;
    }
}
