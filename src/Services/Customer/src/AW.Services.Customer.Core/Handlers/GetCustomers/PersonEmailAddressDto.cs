using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class PersonEmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, PersonEmailAddressDto>();
        }
    }
}