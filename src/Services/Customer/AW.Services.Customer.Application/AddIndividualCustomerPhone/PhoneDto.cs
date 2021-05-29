using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddIndividualCustomerPhone
{
    public class PhoneDto : IMapFrom<Domain.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhoneDto, Domain.PersonPhone>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}