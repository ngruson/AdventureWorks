using MediatR;

namespace AW.Core.Application.Customer.AddCustomerContact
{
    public class AddCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactDto CustomerContact { get; set; }
    }
}