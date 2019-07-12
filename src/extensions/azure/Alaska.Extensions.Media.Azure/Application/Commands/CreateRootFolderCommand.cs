using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class CreateRootFolderCommand : IRequest<MediaFolder>
    {
        public CreateRootFolderCommand(string folderName)
        {
            FolderName = folderName ?? throw new ArgumentNullException(nameof(folderName));
        }

        public string FolderName { get; }
    }
}
