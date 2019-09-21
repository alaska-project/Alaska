using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Commands
{
    internal class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, ContentItem>
    {
        private readonly IContentsProvider _contentsService;
        private readonly IContentsAuthorizationMiddleware _auth;

        public UpdateContentCommandHandler(
            IContentsProvider contentsService,
            IContentsAuthorizationMiddleware auth = null)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
            _auth = auth;
        }

        public async Task<ContentItem> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            if (_auth != null && !_auth.CanWrite(request.Item))
                throw new UnauthorizedAccessException($"Item {request.Item.Info.Id} update not allowed");

            var content = await _contentsService.UpdateContent(request.Item);
            
            return content;
        }
    }
}
