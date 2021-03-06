﻿using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Application.Commands
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
        public MediaFolder Parent { get; }
    }
}
