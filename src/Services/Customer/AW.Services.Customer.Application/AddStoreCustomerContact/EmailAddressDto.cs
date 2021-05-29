using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, Domain.PersonEmailAddress>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}