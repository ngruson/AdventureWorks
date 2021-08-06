using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
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