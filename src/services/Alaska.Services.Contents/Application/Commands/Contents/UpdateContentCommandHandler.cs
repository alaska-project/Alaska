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
        private readonly IContentsService _contentsService;

        public UpdateContentCommandHandler(IContentsService contentsService)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
        }

        public async Task<ContentItem> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var content = await _contentsService.UpdateContent(request.Item);

            return content;
        }
    }
}
