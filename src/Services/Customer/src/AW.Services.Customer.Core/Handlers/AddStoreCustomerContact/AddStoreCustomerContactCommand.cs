using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class AddStoreCustomerContactCommand : IRequest<Unit>
    {
        public AddStoreCustomerContactCommand(string accountNumber, StoreCustomerContactDto customerContact)
        {
            AccountNumber = accountNumber;
            CustomerContact = customerContact;
        }

        public string AccountNumber { get; private init; }
        public StoreCustomerContactDto CustomerContact { get; private init; }
    }
}