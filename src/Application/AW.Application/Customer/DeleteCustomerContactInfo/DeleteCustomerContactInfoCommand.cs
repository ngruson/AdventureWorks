using MediatR;

namespace AW.Application.Customer.DeleteCustomerContactInfo
{
    public class DeleteCustomerContactInfoCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactInfoDto CustomerContactInfo { get; set; }

    }
}