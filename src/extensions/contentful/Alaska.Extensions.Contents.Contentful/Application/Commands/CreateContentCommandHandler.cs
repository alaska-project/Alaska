using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Converters;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Models;
using Contentful.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    internal class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, ContentItemReference>
    {
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentsConverter _converter;
        private readonly ContentQueries _query;

        public CreateContentCommandHandler(
            ContentfulClientsFactory factory,
            ContentsConverter converter,
            ContentQueries query)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public async Task<ContentItemReference> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var emptyEntry = await contentManagementClient.CreateEntry(new Entry<dynamic>(), request.CreationRequest.TemplateId);

            var itemReference = GetItemReference(emptyEntry, request);

            var entry = await _query.GetContentItem(itemReference, PublishingTarget.Preview);

            var contentType = _query.GetContentType(request.CreationRequest.TemplateId);

            var newEntry = _converter.TransformContentItemData(entry, request.CreationRequest.Fields, contentType);

            await contentManagementClient.UpdateEntryForLocale(newEntry, emptyEntry.SystemProperties.Id, request.CreationRequest.Language);

            return itemReference;
        }

        private ContentItemReference GetItemReference(Entry<dynamic> entry, CreateContentCommand request)
        {
            return new ContentItemReference
            {
                Id = entry.SystemProperties.Id,
                Locale = request.CreationRequest.Language,
            };
        }
    }
}
