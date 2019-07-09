using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Utils;
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

        public ContentItemField ReadField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Link,
                Value = field == null ? null : GetLinkedAssetObject(field.sys.id.Value),
            };
        }

        public dynamic WriteField(dynamic field, Field fieldDefinition, ContentItemField fieldValue)
        {
            ItemImageField imageField = FieldSerializationUtil.ConvertDeserializedField<ItemImageField>(fieldValue.Value);
            var newField = FieldSerializationUtil.JsonClone<dynamic>(field);
            newField.sys.id = imageField.InternalId;
            return newField;
        }

        private object GetLinkedAssetObject(string assetId)
        {
            var assetTask = GetLinkedAsset(assetId);
            assetTask.Wait();
            return new ItemImageField
            {
                InternalId = assetId,
                Url = assetTask.Result.File.Url,
            };
        }

        private async Task<Asset> GetLinkedAsset(string assetId)
        {
            return await _clientsFactory.GetContentsClient().GetAsset(assetId, (string)null);
        }
    }
}
