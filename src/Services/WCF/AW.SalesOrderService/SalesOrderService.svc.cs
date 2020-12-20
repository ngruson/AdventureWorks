using AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrder;
using AW.Core.Application.SalesOrder.GetSalesOrders;
using AW.SalesOrderService.Messages.GetSalesOrder;
using AW.SalesOrderService.Messages.ListSalesOrders;
using MediatR;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesOrderService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SalesOrderService(IMediator mediator, IMapper mapper) =>
            (this.mediator, this.mapper) = (mediator, mapper);

        public async Task<ListSalesOrdersResponse> ListSalesOrders(ListSalesOrdersRequest request)
        {
            var query = new GetSalesOrdersQuery
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                CustomerType = mapper.Map<Core.Domain.Sales.CustomerType>(request.CustomerType),
                Territory = request.Territory
            };
            var result = await mediator.Send(query);

            return mapper.Map<ListSalesOrdersResponse>(result);
        }

        public async Task<GetSalesOrderResponse> GetSalesOrder(GetSalesOrderRequest request)
        {
            var salesOrder = await mediator.Send(
                new GetSalesOrderQuery { SalesOrderNumber = request.SalesOrderNumber }
            );

            var response = new GetSalesOrderResponse
            {
                SalesOrder = mapper.Map<Messages.GetSalesOrder.SalesOrder>(salesOrder)
            };

            return response;
        }
    }
}