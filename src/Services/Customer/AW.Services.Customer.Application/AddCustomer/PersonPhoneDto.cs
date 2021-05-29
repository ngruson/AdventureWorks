using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class PersonPhoneDto : IMapFrom<Domain.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhoneDto, Domain.PersonPhone>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}