using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommand : IRequest<Result>
    {
        public UpdateCustomerAddressCommand(Guid customerId, CustomerAddress? customerAddress)
        {
            CustomerId = customerId;
            CustomerAddress = customerAddress;
        }

        public Guid CustomerId { get; private init; }
        public CustomerAddress? CustomerAddress { get; private init; }
    }
}
