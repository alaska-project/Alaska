using Alaska.Services.Contents.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace Alaska.Services.Contents.Application.Services
{
    public class DefaultImageTransformer : IImageTransformer
    {
        public byte[] CreateImageThumbnail(byte[] originalImage, int size, int quality)
        {
            using (var image = new Bitmap(FromBytes(originalImage)))
            {
                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                using (var output = new MemoryStream())
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    var codec = ImageCodecInfo.GetImageDecoders()
                        .FirstOrDefault(x => x.FormatID == ImageFormat.Jpeg.Guid);
                    resized.Save(output, codec, encoderParameters);
                    return output.ToArray();
                }
            }
        }


        private Image FromBytes(byte[] binaryImage) => Image.FromStream(new MemoryStream(binaryImage));
    }
}
