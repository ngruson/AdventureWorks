using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class PersonEmailAddress : IMapFrom<Entities.PersonEmailAddress>
    {
        public PersonEmailAddress() { }
        public PersonEmailAddress(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;

        }
        public EmailAddress? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, Entities.PersonEmailAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.PersonId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
