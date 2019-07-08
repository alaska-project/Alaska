using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Abstractions
{
    public interface IFieldAdapter
    {
        ContentItemField ReadField(dynamic field, Field fieldDefinition);
        dynamic WriteField(ContentItemField field, Field fieldDefinition);
    }
}
