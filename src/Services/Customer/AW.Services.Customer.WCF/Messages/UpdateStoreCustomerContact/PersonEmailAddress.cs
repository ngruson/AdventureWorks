using AutoMapper;
using AW.Services.Customer.Application.UpdateStoreCustomerContact;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact
{
    public class PersonEmailAddress : IMapFrom<EmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, EmailAddressDto>();
        }
    }
}