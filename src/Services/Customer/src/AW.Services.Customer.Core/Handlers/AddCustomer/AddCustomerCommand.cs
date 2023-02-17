using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public AddCustomerCommand()
        {
        }
        public AddCustomerCommand(CustomerDto? customer)
        {
            Customer = customer;
        }

        public CustomerDto? Customer { get; set; }
    }
}
