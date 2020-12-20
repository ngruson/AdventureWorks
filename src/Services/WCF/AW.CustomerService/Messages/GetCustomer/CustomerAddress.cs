using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomer;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.GetCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        [XmlElement(ElementName = "AddressType")]
        public string AddressTypeName { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, CustomerAddress>();
        }
    }
}