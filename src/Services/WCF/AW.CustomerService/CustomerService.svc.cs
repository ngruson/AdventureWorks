using AutoMapper;
using AW.Application.GetCustomer;
using AW.Application.GetCustomers;
using AW.CustomerService.Messages;
using AW.CustomerService.Messages.GetCustomer;
using MediatR;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CustomerService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/CustomerService/1.0")]
    public class CustomerService : ICustomerService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerService(IMediator mediator, IMapper mapper) =>
            (this.mediator, this.mapper) = (mediator, mapper);

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

            var response = mapper.Map<ListCustomersResponse>(customers);
            return response;
        }

        public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)
        {
            var customer = await mediator.Send(
                new GetCustomerQuery { AccountNumber = request.AccountNumber } 
            );

            var response = new GetCustomerResponse
            {
                Customer = mapper.Map<Customer>(customer)
            };

            return response;
        }
    }
}