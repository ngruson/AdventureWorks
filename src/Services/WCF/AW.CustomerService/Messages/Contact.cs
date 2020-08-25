using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customers;

namespace AW.CustomerService.Messages
{
    public class Contact : IMapFrom<ContactDto>
    {
        public string ContactTypeName { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDto, Contact>();
        }
    }
}
