using AutoMapper;
using AW.Services.Customer.Application.AddStoreCustomerContact;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.WCF.Messages.AddStoreCustomerContact
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