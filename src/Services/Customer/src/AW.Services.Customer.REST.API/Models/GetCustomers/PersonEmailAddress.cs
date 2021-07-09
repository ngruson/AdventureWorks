using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }
    }
}