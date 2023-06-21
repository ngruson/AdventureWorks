using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class PersonPhone : IMapFrom<Entities.PersonPhone>
    {
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonPhone, PersonPhone>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.PhoneNumberType == dest.PhoneNumberType);
        }
    }
}