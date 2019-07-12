using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
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
