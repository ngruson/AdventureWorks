using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.AddCustomerAddress;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.AddCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/AddCustomerAddress")]
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>();
        }
    }
}