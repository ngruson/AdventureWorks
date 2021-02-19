using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.DeleteStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, Domain.PersonEmailAddress>();
        }
    }
}