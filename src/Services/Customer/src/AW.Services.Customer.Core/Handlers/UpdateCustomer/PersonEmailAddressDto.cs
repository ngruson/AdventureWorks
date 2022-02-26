using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.Services.SharedKernel.ValueObjects;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public EmailAddress EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, PersonEmailAddressDto>()
                .ReverseMap();
                //.EqualityComparison((src, dest) => src.EmailAddress == dest.EmailAddress);
        }
    }
}