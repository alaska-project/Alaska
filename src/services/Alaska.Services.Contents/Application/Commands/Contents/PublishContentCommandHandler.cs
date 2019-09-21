using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Commands
{
    internal class PublishContentCommandHandler : IRequestHandler<PublishContentCommand, Unit>
    {
        private readonly IContentsProvider _contentsService;
        private readonly IContentsAuthorizationMiddleware _auth;

        public PublishContentCommandHandler(
            IContentsProvider contentsService,
            IContentsAuthorizationMiddleware auth = null)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
            _auth = auth;
        }

        public async Task<Unit> Handle(PublishContentCommand request, CancellationToken cancellationToken)
        {
            await CheckAccessRights(request);
            
            await _contentsService.PublishContent(request.PublishingRequest);

            return Unit.Value;
        }

        private async Task CheckAccessRights(PublishContentCommand request)
        {
            if (_auth == null)
                return;

            foreach (var language in request.PublishingRequest.Language)
            {
                var item = await _contentsService.GetPreviewItem(request.PublishingRequest.ItemId, language);
                if (!_auth.CanPublish(item))
                    throw new UnauthorizedAccessException($"Item {item.Info.Id} update not allowed");
            }
        }
    }
}
