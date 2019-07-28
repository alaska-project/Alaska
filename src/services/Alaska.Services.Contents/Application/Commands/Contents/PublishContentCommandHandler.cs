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
        private readonly IContentsService _contentsService;

        public PublishContentCommandHandler(IContentsService contentsService)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
        }

        public async Task<Unit> Handle(PublishContentCommand request, CancellationToken cancellationToken)
        {
            await _contentsService.PublishContent(request.PublishingRequest);

            return Unit.Value;
        }
    }
}
