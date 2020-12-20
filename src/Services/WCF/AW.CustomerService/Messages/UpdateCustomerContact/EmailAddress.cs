using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerContact
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