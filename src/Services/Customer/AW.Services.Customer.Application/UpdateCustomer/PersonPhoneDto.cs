using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.UpdateCustomer
{
    public class PersonPhoneDto : IMapFrom<Domain.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonPhone, PersonPhoneDto>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.PhoneNumberType == dest.PhoneNumberType);
        }
    }
}