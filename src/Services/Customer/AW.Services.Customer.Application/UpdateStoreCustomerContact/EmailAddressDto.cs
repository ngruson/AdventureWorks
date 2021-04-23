using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.UpdateStoreCustomerContact
{
    public class EmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonEmailAddress, EmailAddressDto>();
        }
    }
}