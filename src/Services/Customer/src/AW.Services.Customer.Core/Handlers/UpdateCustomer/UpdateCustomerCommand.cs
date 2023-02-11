using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public UpdateCustomerCommand()
        {
        }
        public UpdateCustomerCommand(CustomerDto? customer)
        {
            Customer = customer;
        }

        public CustomerDto? Customer { get; private init; }
    }
}