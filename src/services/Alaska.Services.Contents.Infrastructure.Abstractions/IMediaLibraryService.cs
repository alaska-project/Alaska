using Alaska.Services.Contents.Domain.Models.Media;
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
        Task<MediaFolder> CreateRootFolder(string folderName);
        Task<MediaFolder> CreateFolder(string folderName, string parentFolderId);
        Task DeleteFolder(string folderId);

        Task<MediaContent> AddMedia(string mediaName, byte[] mediaContent, string mediaContentType);
        Task DeleteMedia(string mediaId);
    }
}
