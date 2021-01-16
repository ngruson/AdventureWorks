﻿using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class SalesPersonApi : ISalesPersonApi
    {
        private ILogger<SalesPersonApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public SalesPersonApi(
            ILogger<SalesPersonApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<GetSalesPersonResponse> GetSalesPersonAsync(GetSalesPersonRequest request)
        {
            logger.LogInformation("GET: GetSalesPerson request to SalesPerson API");
            var response = await httpRequestFactory.Get($"{baseAddress}/{request.FullName}");
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<GetSalesPersonResponse>();
            }

            return null;
        }

        public async Task<ListSalesPersonsResponse> ListSalesPersonsAsync(ListSalesPersonsRequest request)
        {
            logger.LogInformation("GET: ListSalesPersons request to SalesPerson API");
            var response = await httpRequestFactory.Get($"{baseAddress}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListSalesPersonsResponse>();
            }

            return null;
        }
    }
}