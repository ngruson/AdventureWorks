using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.SharedKernel.AutoMapper;

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