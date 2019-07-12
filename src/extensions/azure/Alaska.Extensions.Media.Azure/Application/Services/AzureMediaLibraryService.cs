using Alaska.Extensions.Media.Azure.Application.Commands;
using Alaska.Extensions.Media.Azure.Application.Query;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Services
{
    internal class AzureMediaLibraryService : IMediaLibraryService
    {
        private readonly IMediator _mediator;
        private readonly AzureMediaLibraryQuery _query;

        public AzureMediaLibraryService(IMediator mediator, AzureMediaLibraryQuery query)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public async Task<MediaContent> AddMedia(string mediaName, byte[] mediaContent, string mediaContentType)
        {
            return await _mediator.Send(new AddMediaCommand(mediaName, mediaContent, mediaContentType));
        }

        public async Task<MediaFolder> CreateFolder(string folderName, MediaFolder parent)
        {
            return await _mediator.Send(new CreateFolderCommand(folderName, parent));
        }

        public async Task DeleteFolder(string folderId)
        {
            await _mediator.Send(new DeleteFolderCommand(folderId));
        }

        public async Task DeleteMedia(string mediaId)
        {
            await _mediator.Send(new DeleteMediaCommand(mediaId));
        }

        public async Task<IEnumerable<MediaFolder>> GetChildrenFolders(MediaFolder folder)
        {
            return await _query.GetChildrenFolders(folder);
        }

        public async Task<IEnumerable<MediaContent>> GetFolderContents(MediaFolder folder)
        {
            return await _query.GetFolderContents(folder);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _query.GetRootFolders();
        }
    }
}
