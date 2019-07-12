using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class DeleteFolderCommand : IRequest<Unit>
    {
        public DeleteFolderCommand(string folderId)
        {
            FolderId = folderId ?? throw new ArgumentNullException(nameof(folderId));
        }

        public string FolderId { get; }
    }
}
