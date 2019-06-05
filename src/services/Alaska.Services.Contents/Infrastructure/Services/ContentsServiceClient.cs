using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Extensions;
using Alaska.Services.Contents.Infrastructure.Settings;
using Alaska.Services.Contents.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Services
{
    public class ContentsServiceClient
    {
        private readonly IOptions<ContentServiceSettings> _settings;

        public ContentsServiceClient(IOptions<ContentServiceSettings> settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<ContentSearchResult> GetContents(ContentsSearchRequest searchRequest)
        {
            using (var client = new HttpClient())
            {
                var url = GetSearchUrl(searchRequest);
                return await client.GetJson<ContentSearchResult>(url);
            }
        }

        private Uri GetSearchUrl(ContentsSearchRequest searchRequest)
        {
            return UriHelper.Build(GetContentsBaseUrl(), new Dictionary<string, object>
            {
                { "id", searchRequest.Id },
                { "depth", searchRequest.Depth },
                { "publishingTarget", searchRequest.PublishingTarget },
                { "language", searchRequest.Language },
            });
        }

        private string GetContentsBaseUrl()
        {
            if (string.IsNullOrEmpty(_settings.Value.RemoteContentsEndpoint))
                throw new InvalidOperationException($"missing {nameof(_settings.Value.RemoteContentsEndpoint)} inside configuration");

            return $"{_settings.Value.RemoteContentsEndpoint.TrimEnd('/')}/alaska/api/contents/getContents";
        }
    }
}
