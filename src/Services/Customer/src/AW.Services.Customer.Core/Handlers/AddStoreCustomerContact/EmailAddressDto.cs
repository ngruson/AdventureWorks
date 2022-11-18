using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public EmailAddress EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, Entities.PersonEmailAddress>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.PersonId, opt => opt.Ignore());
        }
    }
}