using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;

namespace AW.Services.API.CustomerAPI.Models
{
    public class Contact : IMapFrom<CustomerContactDto>
    {
        public string ContactTypeName { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactDto, Contact>();
        }
    }
}
