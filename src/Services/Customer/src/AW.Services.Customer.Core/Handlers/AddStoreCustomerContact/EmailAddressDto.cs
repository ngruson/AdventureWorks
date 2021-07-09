using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, Entities.PersonEmailAddress>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}