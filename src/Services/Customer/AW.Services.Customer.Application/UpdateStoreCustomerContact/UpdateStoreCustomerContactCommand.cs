using MediatR;

namespace AW.Services.Customer.Application.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContactDto CustomerContact { get; set; }
    }
}