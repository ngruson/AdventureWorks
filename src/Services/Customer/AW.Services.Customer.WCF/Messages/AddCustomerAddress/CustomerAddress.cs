using AutoMapper;
using AW.Services.Customer.Application.AddCustomerAddress;
using AW.Services.Customer.Application.Common;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.AddCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/AddCustomerAddress")]
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();
        }
    }
}