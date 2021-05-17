using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.AddStoreCustomerContact;

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