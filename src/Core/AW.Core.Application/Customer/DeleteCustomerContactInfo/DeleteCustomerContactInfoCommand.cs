using MediatR;

namespace AW.Core.Application.Customer.DeleteCustomerContactInfo
{
    public class DeleteCustomerContactInfoCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactInfoDto CustomerContactInfo { get; set; }

    }
}