using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Utils
{
    public static class MediaHelper
    {
        public static string GetBase64FileContent(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
