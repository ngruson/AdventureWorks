using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone
{
    public class Phone : IMapFrom<Entities.PersonPhone>
    {
        public Phone() { }

        public Phone(string phoneNumberType, string phoneNumber) 
        {
            PhoneNumberType = phoneNumberType;
            PhoneNumber = phoneNumber;
        }

        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Phone, Entities.PersonPhone>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForCtorParam("phoneNumberType", opt => opt.MapFrom(src => src.PhoneNumberType))
                .ForCtorParam("phoneNumber", opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
