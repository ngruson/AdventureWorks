using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, PersonEmailAddressDto>()
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress.Value));
        }
    }
}