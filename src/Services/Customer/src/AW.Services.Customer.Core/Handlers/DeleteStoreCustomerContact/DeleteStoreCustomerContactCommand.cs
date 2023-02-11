using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommand : IRequest<Unit>
    {
        public string? AccountNumber { get; set; }
        public StoreCustomerContactDto? CustomerContact { get; set; }
    }
}