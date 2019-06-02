using Alaska.Services.Contents.Domain.Models.Fields;
using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
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
                Value = new ItemImageField
                {
                    Alt = GetField(field).Alt,
                    Class = GetField(field).Class,
                    Url = GetImageUrl(field),
                },
            };
        }

        private string GetImageUrl(ImageField imageField)
        {
            var image = new MediaItem(imageField.MediaItem);
            return StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
        }
    }
}
