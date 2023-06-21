using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result<Customer>>
    {
        public UpdateCustomerCommand()
        {
        }
        public UpdateCustomerCommand(Customer? customer)
        {
            Customer = customer;
        }

        public Customer? Customer { get; private init; }
    }
}
