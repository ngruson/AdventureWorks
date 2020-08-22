using AW.Application.CountSalesOrders;
using AW.Application.GetSalesOrders;
using AW.SalesOrderService.Messages;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesOrderService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IMediator mediator;

        public SalesOrderService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListSalesOrdersResponse> ListSalesOrders(ListSalesOrdersRequest request)
        {
            var query = new GetSalesOrdersQuery
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                CustomerType = request.CustomerType,
                Territory = request.Territory
            };
            var salesOrders = await mediator.Send(query);

            var response = new ListSalesOrdersResponse
            {
                TotalSalesOrders = await mediator.Send(new CountSalesOrdersQuery
                {
                    Territory = request.Territory
                }),
                SalesOrder = salesOrders.ToList(),
            };

            return response;
        }
    }
}