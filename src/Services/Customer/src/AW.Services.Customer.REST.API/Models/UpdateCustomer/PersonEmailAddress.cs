using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, PersonEmailAddressDto>()
                .ReverseMap();
        }
    }
}