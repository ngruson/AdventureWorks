using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommand : IRequest<Result>
    {
        public DeleteStoreCustomerContactCommand() { }
        public DeleteStoreCustomerContactCommand(Guid customerId, Guid contactId) 
        {
            CustomerId = customerId;
            ContactId = contactId;
        }

        public Guid CustomerId { get; set; }
        public Guid ContactId { get; set; }
    }
}
