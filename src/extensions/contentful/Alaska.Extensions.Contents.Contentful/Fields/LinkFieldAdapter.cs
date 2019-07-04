using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Extensions.Contents.Contentful.Services;
using Alaska.Services.Contents.Domain.Models.Fields;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class LinkFieldAdapter : IFieldAdapter
    {
        private readonly ContentfulClientsFactory _clientsFactory;

        public LinkFieldAdapter(ContentfulClientsFactory clientsFactory)
        {
            _clientsFactory = clientsFactory ?? throw new ArgumentNullException(nameof(clientsFactory));
        }

        public ContentItemField AdaptField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Link,
                Value = field == null ? null : GetLinkedAssetObject(field.sys.id.Value),
            };
        }

        private object GetLinkedAssetObject(string assetId)
        {
            var assetTask = GetLinkedAsset(assetId);
            assetTask.Wait();
            return new ItemImageField
            {
                Url = assetTask.Result.File.Url,
            };
        }

        private async Task<Asset> GetLinkedAsset(string assetId)
        {
            return await _clientsFactory.GetContentsClient().GetAsset(assetId, (string)null);
        }
    }
}
