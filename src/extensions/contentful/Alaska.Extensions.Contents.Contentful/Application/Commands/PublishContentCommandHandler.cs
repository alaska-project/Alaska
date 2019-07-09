using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    public class PublishContentCommandHandler : IRequestHandler<PublishContentCommand, Unit>
    {
        private readonly ContentfulClientsFactory _factory;

        public PublishContentCommandHandler(ContentfulClientsFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<Unit> Handle(PublishContentCommand request, CancellationToken cancellationToken)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var entry = await contentManagementClient.GetEntry(request.ContentPublishRequest.ItemId);

            await contentManagementClient.PublishEntry(entry.SystemProperties.Id, entry.SystemProperties.Version.Value);

            return Unit.Value;
        }
    }
}
