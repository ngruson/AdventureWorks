using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.AddCustomerAddress;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.AddCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/AddCustomerAddress")]
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        [XmlElement(ElementName = "AddressType")]
        public string AddressTypeName { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();
        }
    }
}