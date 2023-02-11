using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class PersonPhoneDto : IMapFrom<Entities.PersonPhone>
    {
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonPhone, PersonPhoneDto>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.PhoneNumberType == dest.PhoneNumberType);
        }
    }
}