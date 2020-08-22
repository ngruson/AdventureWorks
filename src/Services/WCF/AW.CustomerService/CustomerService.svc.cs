using AW.Application.CountCustomers;
using AW.Application.GetCustomers;
using AW.CustomerService.Messages;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CustomerService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/CustomerService/1.0")]
    public class CustomerService : ICustomerService
    {
        private readonly IMediator mediator;

        public CustomerService(IMediator mediator) =>
            (this.mediator) = (mediator);

        public async Task<ListCustomersResponse> ListCustomers(ListCustomersRequest request)
        {
            var query = new GetCustomersQuery
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                CustomerType = request.CustomerType,
                Territory = request.Territory
            };
            var customers = await mediator.Send(query);

            var response = new ListCustomersResponse
            {
                TotalCustomers = await mediator.Send(new CountCustomersQuery
                    {
                        Territory = request.Territory
                    }),
                Customer = customers.ToList(),
            };

            return response;
        }
    }
}