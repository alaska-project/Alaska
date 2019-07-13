using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Services.Contents.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Query
{
    internal class AzureMediaLibraryQuery
    {
        private readonly AzureStorageRepository _repository;

        public AzureMediaLibraryQuery(AzureStorageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<MediaFolder>> GetChildrenFolders(string folderId)
        {
            var directory = _repository.GetDirectoryReference(folderId);
            return await _repository.GetChildrenDirectories(directory);
        }

        public async Task<IEnumerable<MediaContent>> GetFolderContents(string folderId)
        {
            var directory = _repository.GetDirectoryReference(folderId);
            return await _repository.GetChildrenBlobs(directory);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _repository.GetRootContainerDirectories();
        }
    }
}
