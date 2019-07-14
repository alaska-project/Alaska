using MediatR;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class AddImageThumbnailCommand : IRequest<CloudBlockBlob>
    {
        public AddImageThumbnailCommand(string name, string contentType, byte[] mediaContent, string folderId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            MediaContent = mediaContent ?? throw new ArgumentNullException(nameof(mediaContent));
            FolderId = folderId ?? throw new ArgumentNullException(nameof(folderId));
        }

        public string Name { get; }
        public string ContentType { get; }
        public byte[] MediaContent { get; }
        public string FolderId { get; }
    }
}
