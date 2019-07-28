using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class DeleteFolderCommand : IRequest<Unit>
    {
        public DeleteFolderCommand(string folderId)
        {
            FolderId = folderId;
        }

        public string FolderId { get; }
    }
}
