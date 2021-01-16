using AutoMapper;
using AW.Core.Abstractions.Api.SalesOrderApi;
using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class SalesOrderServiceWCF : ISalesOrderApi
    {
        private readonly ILogger<SalesOrderServiceWCF> logger;
        private readonly IMapper mapper;
        private readonly SalesOrderService.ISalesOrderService salesOrderService;

        public SalesOrderServiceWCF(
            ILogger<SalesOrderServiceWCF> logger,
            IMapper mapper,
            SalesOrderService.ISalesOrderService salesOrderService
        ) => (this.logger, this.mapper, this.salesOrderService) = (logger, mapper, salesOrderService);

        public async Task<ListSalesOrdersResponse> ListSalesOrdersAsync(ListSalesOrdersRequest request)
        {
            logger.LogInformation("Mapping to ListSalesOrdersRequest");
            var req = mapper.Map<SalesOrderService.ListSalesOrdersRequest>(request);

            logger.LogInformation("Calling ListSalesOrders operation of SalesOrder web service");
            var response = await salesOrderService.ListSalesOrdersAsync(req);
            logger.LogInformation("ListSalesOrders operation executed succesfully");

            return mapper.Map<ListSalesOrdersResponse>(response);
        }

        public async Task<GetSalesOrderResponse> GetSalesOrderAsync(GetSalesOrderRequest request)
        {
            logger.LogInformation("Mapping to GetSalesOrderRequest");
            var req = mapper.Map<SalesOrderService.GetSalesOrderRequest>(request);

            logger.LogInformation("Calling GetSalesOrder operation of SalesOrder web service");
            var response = await salesOrderService.GetSalesOrderAsync(req);
            logger.LogInformation("GetSalesOrder operation executed succesfully");

            return mapper.Map<GetSalesOrderResponse>(response);
        }
    }
}