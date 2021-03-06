﻿using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IMediaLibraryService
    {
        Task<IEnumerable<MediaFolder>> GetRootFolders();
        Task<IEnumerable<MediaFolder>> GetChildrenFolders(string folderId);
        Task<IEnumerable<MediaContent>> GetFolderContents(string folderId);
        Task<MediaFolder> CreateFolder(string folderName, string parentFolderId);
        Task<MediaFolder> CreateRootFolder(string folderName);
        Task DeleteFolder(string folderId);
        Task<MediaContent> GetMedia(string mediaId);
        Task<MediaContent> AddMedia(MediaCreationRequest mediaContent);
        Task DeleteMedia(string mediaId);
    }
}
