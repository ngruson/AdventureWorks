using AW.Services.Customer.Application.DeleteCustomerAddress;
using System.Xml.Serialization;
using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.DeleteCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/DeleteCustomerAddress")]
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