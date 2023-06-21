using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact
{
    public class CreateStoreCustomerContactCommand : IRequest<Result>
    {
        public CreateStoreCustomerContactCommand(Guid customerId, StoreCustomerContact customerContact)
        {
            CustomerId = customerId;
            CustomerContact = customerContact;
        }

        public Guid CustomerId { get; private init; }
        public StoreCustomerContact CustomerContact { get; private init; }
    }
}
