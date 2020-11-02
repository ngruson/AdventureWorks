using MediatR;

namespace AW.Application.Customer.AddCustomerContactInfo
{
    public class AddCustomerContactInfoCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerContactInfoDto CustomerContactInfo { get; set; }
    }
}