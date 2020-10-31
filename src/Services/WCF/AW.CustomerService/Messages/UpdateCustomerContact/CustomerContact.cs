using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.UpdateCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerContact
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/UpdateCustomerContact")]
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