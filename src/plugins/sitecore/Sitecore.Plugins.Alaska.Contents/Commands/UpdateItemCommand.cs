using Alaska.Services.Contents.Domain.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Commands
{
    internal class UpdateItemCommand
    {
        public UpdateItemCommand(ContentItem item)
        {
            Item = item;
        }

        public ContentItem Item { get; }
    }
}
