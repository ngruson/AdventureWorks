using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.DeleteCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.DeleteCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteCustomerContactRequest : IMapFrom<DeleteCustomerContactCommand>
    {
        public string AccountNumber { get; set; }
        public string ContactType { get; set; }
        public DeleteContact Contact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCustomerContactRequest, DeleteCustomerContactCommand>()
                .ForMember(m => m.ContactTypeName, opt => opt.MapFrom(src => src.ContactType));
        }
    }
}