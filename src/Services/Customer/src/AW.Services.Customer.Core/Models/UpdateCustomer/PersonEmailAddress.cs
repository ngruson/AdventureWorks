using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public EmailAddress EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, PersonEmailAddressDto>()
                .ReverseMap();
        }
    }
}