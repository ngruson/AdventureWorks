using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Result>
    {
        public DeleteCustomerAddressCommand(Guid customerId, Guid addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
    }
}
