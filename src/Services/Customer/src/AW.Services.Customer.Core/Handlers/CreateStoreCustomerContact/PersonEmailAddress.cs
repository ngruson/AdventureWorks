using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact
{
    public class PersonEmailAddress : IMapFrom<Entities.PersonEmailAddress>
    {
        public EmailAddress? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, Entities.PersonEmailAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.PersonId, opt => opt.Ignore());
        }
    }
}
