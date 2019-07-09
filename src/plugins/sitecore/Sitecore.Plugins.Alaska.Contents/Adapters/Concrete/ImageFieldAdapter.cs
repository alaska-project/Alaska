using Alaska.Services.Contents.Domain.Models.Fields;
using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using Sitecore.Plugins.Alaska.Contents.Settings;
using Sitecore.Plugins.Alaska.Contents.Utils;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Adapters.Concrete
{
    internal class ImageFieldAdapter : FieldAdapter<ImageField>
    {
        public override ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Image,
                Value = string.IsNullOrEmpty(field.Value) ? null : GetImageData(field),
            };
        }

        public override void UpdateField(ContentItemField value, Field field)
        {
            //TODO
        }

        private ItemImageField GetImageData(ImageField field)
        {
            return new ItemImageField
            {
                Alt = field.Alt,
                Class = field.Class,
                Url = GetImageAbsoluteUrl(field),
                InternalId = GetImageInternalId(field),
            };
        }

        private string GetImageInternalId(ImageField imageField)
        {
            return imageField.IsInternal ? imageField.MediaID.ToString() : null;
        }

        private string GetImageAbsoluteUrl(ImageField imageField)
        {
            var url = GetImageUrl(imageField);
            if (UriHelper.IsAbsoluteUrl(url))
                return url;

            return UriHelper.BuildAbsoluteUrl(ContentsSettings.Current.DefaultAbsolutePath, url);
        }

        private string GetImageUrl(ImageField imageField)
        {
            var image = new MediaItem(imageField.MediaItem);
            return StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
        }

        public override ImageField GetField(Field field)
        {
            return (ImageField)field;
        }
    }
}
