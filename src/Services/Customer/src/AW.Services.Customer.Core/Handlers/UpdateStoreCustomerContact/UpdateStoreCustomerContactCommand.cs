using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommand : IRequest<Result>
    {
        public Guid CustomerId { get; private init; }
        public StoreCustomerContact CustomerContact { get; private init; }

        public UpdateStoreCustomerContactCommand(Guid customerId, StoreCustomerContact customerContact)
        {
            CustomerId = customerId;
            CustomerContact = customerContact;
        }
    }
}
