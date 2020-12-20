using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomerAddress;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/UpdateCustomerAddress")]
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