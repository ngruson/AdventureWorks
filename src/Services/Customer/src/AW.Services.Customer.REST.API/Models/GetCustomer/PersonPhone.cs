using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class PersonPhone : IMapFrom<PersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}