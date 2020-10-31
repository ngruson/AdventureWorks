using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.AddCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.AddCustomerContact
{
    public class EmailAddress : IMapFrom<EmailAddressDto>
    {
        [XmlElement(ElementName = "EmailAddress")]
        public string EmailAddress1 { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddress, EmailAddressDto>()
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress1));
        }
    }
}