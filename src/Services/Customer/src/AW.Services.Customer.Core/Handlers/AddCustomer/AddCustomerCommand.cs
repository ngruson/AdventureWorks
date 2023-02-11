using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public AddCustomerCommand(CustomerDto? customer)
        {
            Customer = customer;
        }

        public CustomerDto? Customer { get; private init; }
    }
}