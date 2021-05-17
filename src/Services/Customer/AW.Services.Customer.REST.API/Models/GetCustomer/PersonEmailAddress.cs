using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }
    }
}