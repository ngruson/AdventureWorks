using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommand : IRequest<Unit>
    {
        public UpdateCustomerAddressCommand(string accountNumber, CustomerAddressDto? customerAddress)
        {
            AccountNumber = accountNumber;
            CustomerAddress = customerAddress;
        }

        public string AccountNumber { get; private init; }
        public CustomerAddressDto? CustomerAddress { get; private init; }
    }
}