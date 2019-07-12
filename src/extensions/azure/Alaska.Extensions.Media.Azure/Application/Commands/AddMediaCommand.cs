using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class AddMediaCommand : IRequest<MediaContent>
    {
        public AddMediaCommand(string mediaName, byte[] mediaContent, string mediaContentType)
        {
            Name = mediaName ?? throw new ArgumentNullException(nameof(mediaName));
            Content = mediaContent ?? throw new ArgumentNullException(nameof(mediaContent));
            ContentType = mediaContentType;
        }

        public string Name { get; }
        public byte[] Content { get; }
        public string ContentType { get; }
    }
}
