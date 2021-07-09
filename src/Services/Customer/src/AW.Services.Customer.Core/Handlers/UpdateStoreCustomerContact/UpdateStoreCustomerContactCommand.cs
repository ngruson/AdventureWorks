using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContactDto CustomerContact { get; set; }
    }
}