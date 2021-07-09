using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddressDto, Entities.PersonEmailAddress>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}