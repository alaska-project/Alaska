using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class DeleteMediaCommand : IRequest<Unit>
    {
        public DeleteMediaCommand(string mediaId)
        {
            MediaId = mediaId ?? throw new ArgumentNullException(nameof(mediaId));
        }

        public string MediaId { get; }
    }
}
