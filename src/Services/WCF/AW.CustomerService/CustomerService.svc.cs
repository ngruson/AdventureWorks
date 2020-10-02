using AutoMapper;
using AW.Application.Customer.GetCustomer;
using AW.Application.Customer.GetCustomers;
using AW.Application.Customer.UpdateCustomer;
using AW.Application.Customer;
using AW.CustomerService.Messages;
using AW.CustomerService.Messages.GetCustomer;
using AW.CustomerService.Messages.ListCustomers;
using AW.CustomerService.Messages.UpdateCustomer;
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

        public async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = await mediator.Send(
                new UpdateCustomerCommand
                {
                    Customer = mapper.Map<CustomerDto>(request.Customer)
                });

            return new UpdateCustomerResponse
            {
                Customer = mapper.Map<UpdateCustomer>(customer)
            };
        }
    }
}