using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class CreateFolderCommand : IRequest<MediaFolder>
    {
        public CreateFolderCommand(string folderName, MediaFolder parent)
        {
            FolderName = folderName ?? throw new ArgumentNullException(nameof(folderName));
            Parent = parent;
        }

        public string FolderName { get; }
        public MediaFolder Parent { get; }
    }
}
