using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class PersonPhoneDto : IMapFrom<Domain.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}