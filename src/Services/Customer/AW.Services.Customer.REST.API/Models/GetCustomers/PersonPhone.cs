using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class PersonPhone : IMapFrom<PersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}