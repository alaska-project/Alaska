using Alaska.Extensions.Contents.Contentful.Application.Extensions;
using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Converters;
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
        private readonly ContentsConverter _converter;
        private readonly ContentQueries _query;

        public UpdateItemCommandHandler(
            ContentfulClientsFactory factory,
            ContentsConverter converter,
            ContentQueries query)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var entry = await _query.GetContentItem(request.ContentItem.GetReference(), PublishingTarget.Preview);

            var contentType = _query.GetContentType(request.ContentItem.Info.TemplateId);

            var newEntry = _converter.TransformContentItem(entry, request.ContentItem, contentType);

            await contentManagementClient.UpdateEntryForLocale(newEntry, request.ContentItem.Info.Id, request.ContentItem.Info.Language);

            return Unit.Value;
        }
    }
}
