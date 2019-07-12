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
        Task<IEnumerable<MediaFolder>> GetChildrenFolders(MediaFolder folder);
        Task<IEnumerable<MediaContent>> GetFolderContents(MediaFolder folder);
        Task<MediaFolder> CreateFolder(string folderName, MediaFolder parent);
        Task DeleteFolder(string folderId);

        Task<MediaContent> AddMedia(string mediaName, byte[] mediaContent, string mediaContentType);
        Task DeleteMedia(string mediaId);
    }
}
