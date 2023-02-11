using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
{
    public class PersonPhone : IMapFrom<PersonPhoneDto>
    {
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, PersonPhoneDto>()
                .ReverseMap();
        }
    }
}