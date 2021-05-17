using AutoMapper;
using AW.Common.AutoMapper;

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