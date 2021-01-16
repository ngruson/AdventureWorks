using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi.ListContactTypes;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class ContactTypeApi : IContactTypeApi
    {
        private ILogger<ContactTypeApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public ContactTypeApi(
            ILogger<ContactTypeApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<ListContactTypesResponse> ListContactTypesAsync()
        {
            logger.LogInformation("GET: ListContactTypes request to ContactType API");
            var response = await httpRequestFactory.Get($"{baseAddress}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListContactTypesResponse>();
            }

            return null;
        }
    }
}