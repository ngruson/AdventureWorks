using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomerAddress;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/UpdateCustomerAddress")]
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