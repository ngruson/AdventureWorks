using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonEmailAddress, PersonEmailAddressDto>();
        }
    }
}