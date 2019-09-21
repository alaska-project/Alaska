using Alaska.Services.Contents.Application.Commands.Media;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Services
{
    internal class MediaLibraryService : IMediaLibraryService
    {
        private readonly IMediator _mediator;
        private readonly IMediaQueries _mediaQueries;

        public MediaLibraryService(
            IMediator mediator,
            IMediaQueries mediaQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mediaQueries = mediaQueries ?? throw new ArgumentNullException(nameof(mediaQueries));
        }

        public async Task<MediaContent> AddMedia(MediaCreationRequest mediaContent)
        {
            return await _mediator.Send(new AddMediaCommand(mediaContent));
        }

        public async Task<MediaFolder> CreateFolder(string folderName, string parentFolderId)
        {
            return await _mediator.Send(new CreateFolderCommand(folderName, parentFolderId));
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
            return await _mediaQueries.GetChildrenFolders(folderId);
        }

        public async Task<IEnumerable<MediaContent>> GetFolderContents(string folderId)
        {
            return await _mediaQueries.GetFolderContents(folderId);
        }

        public async Task<MediaContent> GetMedia(string mediaId)
        {
            return await _mediaQueries.GetMedia(mediaId);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootFolders()
        {
            return await _mediaQueries.GetRootFolders();
        }
    }
}
