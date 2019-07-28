using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class CreateFolderCommand : IRequest<MediaFolder>
    {
        public CreateFolderCommand(string folderName, string parentFolderId)
        {
            FolderName = folderName ?? throw new ArgumentNullException(nameof(folderName));
            ParentFolderId = parentFolderId;
        }

        public string FolderName { get; }
        public string ParentFolderId { get; }
    }
}
