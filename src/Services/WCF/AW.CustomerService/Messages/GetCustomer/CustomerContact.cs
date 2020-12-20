using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomer;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.GetCustomer
{
    public class CustomerContact : IMapFrom<CustomerContactDto>
    {
        [XmlElement(ElementName = "ContactType")]
        public string ContactTypeName { get; set; }
        public Contact Contact { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactDto, CustomerContact>();
        }
    }
}