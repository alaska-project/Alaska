using Alaska.Extensions.Contents.Contentful.Application.Extensions;
using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Models.Items;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentQueries _query;

        public UpdateItemCommandHandler(
            ContentfulClientsFactory factory,
            ContentQueries query)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var contentType = _query.GetContentType(request.ContentItem.Info.TemplateId);

            var entry = _query.GetContentItem(request.ContentItem.GetReference(), PublishingTarget.Preview);
            //var entry = _converter.ConvertToContentEntry(contentItem, contentType);

            await contentManagementClient.UpdateEntryForLocale(entry, request.ContentItem.Info.Id, request.ContentItem.Info.Language);

            return Unit.Value;
        }
    }
}
