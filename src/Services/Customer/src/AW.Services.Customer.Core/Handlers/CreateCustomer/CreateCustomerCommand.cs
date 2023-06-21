using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<CreatedCustomer>>
    {
        public CreateCustomerCommand()
        {
        }
        public CreateCustomerCommand(Customer? customer)
        {
            Customer = customer;
        }

        public Customer? Customer { get; set; }
    }
}
