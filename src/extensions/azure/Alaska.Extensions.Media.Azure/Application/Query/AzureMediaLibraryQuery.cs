using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Services.Contents.Domain.Models.Media;
using System;
using System.Collections.Generic;
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
            if (!await _repository.ExistsContainer(folderId))
                throw new InvalidOperationException($"Container {folderId} not found");

            var container = await _repository.GetContainer(folderId);
            return await _repository.GetContainerDirectories(container);
        }

        public Task<IEnumerable<MediaContent>> GetFolderContents(string folderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _repository.GetRootContainerDirectories();
        }

        
    }
}
