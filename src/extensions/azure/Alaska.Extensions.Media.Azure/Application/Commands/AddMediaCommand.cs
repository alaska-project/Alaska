using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class AddMediaCommand : IRequest<MediaContent>
    {
        public AddMediaCommand(string name, string contentType, byte[] mediaContent, string folderId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            Content = mediaContent ?? throw new ArgumentNullException(nameof(mediaContent));
            FolderId = folderId ?? throw new ArgumentNullException(nameof(folderId));
        }

        public string Name { get; }
        public string ContentType { get; }
        public byte[] Content { get; }
        public string FolderId { get; }
    }
}
