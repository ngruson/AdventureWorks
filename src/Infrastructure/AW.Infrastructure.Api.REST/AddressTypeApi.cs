using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class AddressTypeApi : IAddressTypeApi
    {
        private ILogger<AddressTypeApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public AddressTypeApi(
            ILogger<AddressTypeApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<ListAddressTypesResponse> ListAddressTypesAsync()
        {
            logger.LogInformation("GET: ListAddressTypes request to AddressType API");
            var response = await httpRequestFactory.Get($"{baseAddress}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListAddressTypesResponse>();
            }

            return null;
        }
    }
}