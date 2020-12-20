using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomers;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class ContactInfo : IMapFrom<ContactInfoDto>
    {
        public ContactInfoChannelType ContactInfoChannelType { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactInfoDto, ContactInfo>();
        }
    }
}