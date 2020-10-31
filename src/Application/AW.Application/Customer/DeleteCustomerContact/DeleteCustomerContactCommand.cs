using MediatR;

namespace AW.Application.Customer.DeleteCustomerContact
{
    public class DeleteCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string ContactTypeName { get; set; }
        public ContactDto Contact { get; set; }
        
    }
}