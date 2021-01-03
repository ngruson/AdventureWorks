using AutoMapper;
using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class SalesPersonServiceAdapter : ISalesPersonApi
    {
        private readonly ILogger<SalesPersonServiceAdapter> logger;
        private readonly IMapper mapper;
        private readonly SalesPersonService.ISalesPersonService salesPersonService;

        public SalesPersonServiceAdapter(
            ILogger<SalesPersonServiceAdapter> logger,
            IMapper mapper,
            SalesPersonService.ISalesPersonService salesPersonService
        ) => (this.logger, this.mapper, this.salesPersonService) = (logger, mapper, salesPersonService);

        public async Task<ListSalesPersonsResponse> ListSalesPersonsAsync(ListSalesPersonsRequest request)
        {
            logger.LogInformation("Mapping to ListSalesPersonsRequest");
            var req = mapper.Map<SalesPersonService.ListSalesPersonsRequest1>(request);

            logger.LogInformation("Calling ListSalesPersons operation of SalesPerson web service");
            var response = await salesPersonService.ListSalesPersonsAsync(req);
            logger.LogInformation("ListSalesPersons operation executed succesfully");

            return mapper.Map<ListSalesPersonsResponse>(response);
        }

        public async Task<GetSalesPersonResponse> GetSalesPersonAsync(GetSalesPersonRequest request)
        {
            logger.LogInformation("Mapping to GetSalesPersonRequest");
            var req = mapper.Map<SalesPersonService.GetSalesPersonRequest>(request);

            logger.LogInformation("Calling GetSalesPerson operation of SalesPerson web service");
            var response = await salesPersonService.GetSalesPersonAsync(req);
            logger.LogInformation("GetSalesPerson operation executed succesfully");

            return mapper.Map<GetSalesPersonResponse>(response);
        }
    }
}