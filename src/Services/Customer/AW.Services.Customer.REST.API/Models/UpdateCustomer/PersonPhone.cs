using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class PersonPhone : IMapFrom<PersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, PersonPhoneDto>()
                .ReverseMap();
        }
    }
}