using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.AddStoreCustomerContact;

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