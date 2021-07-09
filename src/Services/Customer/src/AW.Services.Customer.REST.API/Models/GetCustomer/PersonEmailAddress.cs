using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }
    }
}