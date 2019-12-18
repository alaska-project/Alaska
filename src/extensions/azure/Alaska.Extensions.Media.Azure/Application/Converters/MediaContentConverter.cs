using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alaska.Common.Utils;
using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Alaska.Extensions.Media.Azure.Application.Converters
{
    internal class MediaContentConverter
    {
        private readonly IOptions<AzureMediaStorageOptions> _storageConfig;

        public MediaContentConverter(IOptions<AzureMediaStorageOptions> storageConfig)
        {
            _storageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
        }

        public MediaContent ConvertContent(CloudBlockBlob content, CloudBlockBlob thumbnail)
        {
            return new MediaContent
            {
                Id = content.Name,
                Name = content.Name.TrimEnd('/').Split('/').Last(),
                Url = ProcessUrl(content.Uri),
                ThumbnailUrl = thumbnail != null ? ProcessUrl(thumbnail.Uri) : null,
                ContentType = content.Properties.ContentType,
            };
        }

        private string ProcessUrl(Uri uri)
        {
            if (!IsCdnEnabled())
                return uri.ToString();

            return UriHelper.ReplaceHost(uri, _storageConfig.Value.CdnSettings.Hostname);
        }

        private bool IsCdnEnabled() => _storageConfig.Value.CdnSettings?.Enabled ?? false;
    }
}
