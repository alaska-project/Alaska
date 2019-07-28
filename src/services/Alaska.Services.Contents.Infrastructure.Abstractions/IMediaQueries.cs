using Alaska.Services.Contents.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IMediaQueries
    {
        Task<IEnumerable<MediaFolder>> GetRootFolders();
        Task<IEnumerable<MediaFolder>> GetChildrenFolders(string folderId);
        Task<IEnumerable<MediaContent>> GetFolderContents(string folderId);
        Task<MediaContent> GetMedia(string mediaId);
    }
}
