using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Domain.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class AddMediaCommand : IRequest<MediaContent>
    {
        public AddMediaCommand(MediaCreationRequest mediaContent)
        {
            MediaContent = mediaContent ?? throw new ArgumentNullException(nameof(mediaContent));
        }

        public MediaCreationRequest MediaContent { get; }
    }
}
