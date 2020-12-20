using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.AddCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.AddCustomerContact
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/AddCustomerContact")]
    public class CustomerContact : IMapFrom<CustomerContactDto>
    {
        [XmlElement(ElementName = "ContactType")]
        public string ContactTypeName { get; set; }
        public Contact Contact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContact, CustomerContactDto>();
        }
    }
}