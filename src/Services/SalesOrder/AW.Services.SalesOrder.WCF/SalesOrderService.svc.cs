using AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrder;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using AW.Services.SalesOrder.WCF.Messages.GetSalesOrder;
using AW.Services.SalesOrder.WCF.Messages.ListSalesOrders;
using MediatR;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.WCF
{
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