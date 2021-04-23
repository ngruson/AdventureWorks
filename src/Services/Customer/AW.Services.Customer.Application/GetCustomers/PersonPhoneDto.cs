using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class PersonPhoneDto : IMapFrom<Domain.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonPhone, PersonPhoneDto>();
        }
    }
}