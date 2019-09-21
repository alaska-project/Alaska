using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Queries
{
    internal class MediaQueries : IMediaQueries
    {
        private readonly IMediaLibraryProvider _mediaLibrary;

        public MediaQueries(IMediaLibraryProvider mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<IEnumerable<MediaFolder>> GetChildrenFolders(string folderId)
        {
            return await _mediaLibrary.GetChildrenFolders(folderId);
        }

        public async Task<IEnumerable<MediaContent>> GetFolderContents(string folderId)
        {
            return await _mediaLibrary.GetFolderContents(folderId);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _mediaLibrary.GetRootFolders();
        }

        public async Task<MediaContent> GetMedia(string mediaId)
        {
            return await _mediaLibrary.GetMedia(mediaId);
        }
    }
}
