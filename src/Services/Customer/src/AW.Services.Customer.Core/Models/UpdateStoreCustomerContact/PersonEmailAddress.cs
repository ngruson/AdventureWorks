using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Models.UpdateStoreCustomerContact
{
    public class PersonEmailAddress : IMapFrom<EmailAddressDto>
    {
        public EmailAddress EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, EmailAddressDto>();
        }
    }
}