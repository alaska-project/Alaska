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
    internal class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, ContentItem>
    {
        private readonly IContentsService _contentsService;
        private readonly IContentsAuthorizationMiddleware _auth;

        public CreateContentCommandHandler(
            IContentsService contentsService,
            IContentsAuthorizationMiddleware auth = null)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
            _auth = auth;
        }

        public async Task<ContentItem> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            if (_auth != null && !_auth.CanCreate(request.Request))
                throw new UnauthorizedAccessException($"Item {request.Request.TemplateId} creation not allowed");

            var content = await _contentsService.CreateContent(request.Request);
            return content;
        }
    }
}
