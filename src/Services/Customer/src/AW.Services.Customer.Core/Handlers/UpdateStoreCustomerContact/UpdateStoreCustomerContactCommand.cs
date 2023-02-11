using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; private init; }
        public StoreCustomerContactDto CustomerContact { get; private init; }

        public UpdateStoreCustomerContactCommand(string accountNumber, StoreCustomerContactDto customerContact)
        {
            AccountNumber = accountNumber;
            CustomerContact = customerContact;
        }
    }
}