using Alaska.Services.Contents.Domain.Models.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Commands
{
    
    
    internal class PublishItemCommand
    {
        public PublishItemCommand(PublishContentRequest publishingRequest)
        {
            PublishingRequest = publishingRequest;
        }

        public string ItemId { get; }
        public string Target { get; }
        public PublishScope Scope { get; }
        public PublishContentRequest PublishingRequest { get; }
    }
}
