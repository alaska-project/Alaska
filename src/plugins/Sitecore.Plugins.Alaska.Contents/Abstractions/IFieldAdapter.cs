using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Abstractions
{
    public interface IFieldAdapter
    {
        ContentItemField AdaptField(Field field);
        void UpdateField(ContentItemField value, Field field);
    }
}
