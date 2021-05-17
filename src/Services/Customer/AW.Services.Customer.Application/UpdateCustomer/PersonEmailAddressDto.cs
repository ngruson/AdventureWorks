using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.UpdateCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonEmailAddress, PersonEmailAddressDto>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.EmailAddress == dest.EmailAddress);
        }
    }
}