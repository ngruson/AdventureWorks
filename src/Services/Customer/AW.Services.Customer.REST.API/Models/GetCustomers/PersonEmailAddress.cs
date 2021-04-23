using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }
    }
}