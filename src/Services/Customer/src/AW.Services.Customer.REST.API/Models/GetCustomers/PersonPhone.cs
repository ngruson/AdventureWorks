using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class PersonPhone : IMapFrom<PersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}