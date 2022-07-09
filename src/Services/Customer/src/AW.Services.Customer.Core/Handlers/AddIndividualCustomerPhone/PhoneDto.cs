using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone
{
    public class PhoneDto : IMapFrom<Entities.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhoneDto, Entities.PersonPhone>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForCtorParam("phoneNumberType", opt => opt.MapFrom(src => src.PhoneNumberType))
                .ForCtorParam("phoneNumber", opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}