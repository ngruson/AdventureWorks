using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddCustomerAddress
{
    public class AddCustomerAddressCommand : IRequest<Unit>
    {
        public AddCustomerAddressCommand(string accountNumber, CustomerAddressDto customerAddress)
        {
            AccountNumber = accountNumber;
            CustomerAddress = customerAddress;
        }

        public string AccountNumber { get; private init; }
        public CustomerAddressDto CustomerAddress { get; private init; }
    }
}