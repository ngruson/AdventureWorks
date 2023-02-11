using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Unit>
    {
        public DeleteCustomerAddressCommand(string accountNumber, string addressType)
        {
            AccountNumber = accountNumber;
            AddressType = addressType;
        }

        public string AccountNumber { get; set; }
        public string AddressType { get; set; }
    }
}