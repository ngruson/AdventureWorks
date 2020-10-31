using MediatR;

namespace AW.Application.Customer.UpdateCustomerContact
{
    public class UpdateCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactDto CustomerContact { get; set; }
    }
}