using AutoMapper;
using AW.Application.GetSalesOrders;
using AW.SalesOrderService.Messages;
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
                CustomerType = request.CustomerType,
                Territory = request.Territory
            };
            var result = await mediator.Send(query);

            return mapper.Map<ListSalesOrdersResponse>(result);
        }
    }
}