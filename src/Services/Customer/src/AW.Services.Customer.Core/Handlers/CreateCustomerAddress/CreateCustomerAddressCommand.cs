using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.CreateCustomerAddress
{
    public class CreateCustomerAddressCommand : IRequest<Result>
    {
        public CreateCustomerAddressCommand(Guid customerId, CustomerAddress customerAddress)
        {
            CustomerId = customerId;
            CustomerAddress = customerAddress;
        }

        public Guid CustomerId { get; private init; }
        public CustomerAddress CustomerAddress { get; private init; }
    }
}
