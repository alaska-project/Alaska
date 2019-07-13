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

        public async Task<MediaContent> AddMedia(string name, string contentType, byte[] mediaContent, string folderId)
        {
            return await _mediator.Send(new AddMediaCommand(name, contentType, mediaContent, folderId));
        }

        public async Task<MediaFolder> CreateFolder(string folderName, string parentId)
        {
            return await _mediator.Send(new CreateFolderCommand(folderName, parentId));
        }

        public async Task<MediaFolder> CreateRootFolder(string folderName)
        {
            return await _mediator.Send(new CreateRootFolderCommand(folderName));
        }

        public async Task DeleteFolder(string folderId)
        {
            await _mediator.Send(new DeleteFolderCommand(folderId));
        }

        public async Task DeleteMedia(string mediaId)
        {
            await _mediator.Send(new DeleteMediaCommand(mediaId));
        }

        public async Task<IEnumerable<MediaFolder>> GetChildrenFolders(string folderId)
        {
            return await _query.GetChildrenFolders(folderId);
        }

        public async Task<IEnumerable<MediaContent>> GetFolderContents(string folderId)
        {
            return await _query.GetFolderContents(folderId);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _query.GetRootFolders();
        }
    }
}
