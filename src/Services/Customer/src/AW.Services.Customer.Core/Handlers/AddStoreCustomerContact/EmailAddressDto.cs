using AutoMapper;
using AW.Services.SharedKernel.ValueObjects;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public EmailAddress EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, Entities.PersonEmailAddress>();
        }
    }
}