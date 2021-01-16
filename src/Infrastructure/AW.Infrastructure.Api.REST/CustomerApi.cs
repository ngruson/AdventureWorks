using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class CustomerApi : ICustomerApi
    {
        private ILogger<CustomerApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public CustomerApi(
            ILogger<CustomerApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task AddCustomerAddressAsync(AddCustomerAddressRequest request)
        {
            logger.LogInformation("POST: AddCustomerAddress request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/AddCustomerAddress", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task AddCustomerContactAsync(AddCustomerContactRequest request)
        {
            logger.LogInformation("POST: AddCustomerContact request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/AddCustomerContact", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task AddCustomerContactInfoAsync(AddCustomerContactInfoRequest request)
        {
            logger.LogInformation("POST: AddCustomerContactInfo request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/AddCustomerContactInfo", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task DeleteCustomerAddressAsync(DeleteCustomerAddressRequest request)
        {
            logger.LogInformation("POST: DeleteCustomerAddress request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/DeleteCustomerAddress", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task DeleteCustomerContactAsync(DeleteCustomerContactRequest request)
        {
            logger.LogInformation("POST: DeleteCustomerContact request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/DeleteCustomerContact", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task DeleteCustomerContactInfoAsync(DeleteCustomerContactInfoRequest request)
        {
            logger.LogInformation("POST: DeleteCustomerContactInfo request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/DeleteCustomerContactInfo", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request)
        {
            logger.LogInformation("GET: GetCustomer request to Customer API");
            var response = await httpRequestFactory.Get($"{baseAddress}/{request.AccountNumber}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<GetCustomerResponse>();
            }

            return null;
        }

        public async Task<ListCustomersResponse> ListCustomersAsync(ListCustomersRequest request)
        {
            logger.LogInformation("GET: ListCustomers request to Customer API");
            string requestUri = $"{baseAddress}?pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            if (request.CustomerType.HasValue)
                requestUri += $"&customerType={request.CustomerType.Value}";
            if (!string.IsNullOrEmpty(request.Territory))
                requestUri += $"&territory={request.Territory}";
            var response = await httpRequestFactory.Get(requestUri, Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListCustomersResponse>();
            }

            return null;
        }

        public async Task UpdateCustomerAddressAsync(UpdateCustomerAddressRequest request)
        {
            logger.LogInformation("POST: UpdateCustomerAddress request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/UpdateCustomerAddress", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            logger.LogInformation("POST: UpdateCustomer request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/UpdateCustomer", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }

        public async Task UpdateCustomerContactAsync(UpdateCustomerContactRequest request)
        {
            logger.LogInformation("POST: UpdateCustomerContact request to Customer API");
            var response = await httpRequestFactory.Post($"{baseAddress}/UpdateCustomerContact", request);
            logger.LogInformation($"POST: Response = {response.StatusCode} ({response.ReasonPhrase})");
        }
    }
}