using MediatR;

namespace AW.Application.Customer.AddCustomerContact
{
    public class AddCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactDto CustomerContact { get; set; }
    }
}