using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, EmailAddressDto>()
                .ReverseMap();
        }
    }
}