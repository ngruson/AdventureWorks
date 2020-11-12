using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomer
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