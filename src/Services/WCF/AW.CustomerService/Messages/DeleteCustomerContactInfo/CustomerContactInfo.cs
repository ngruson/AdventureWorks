using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.DeleteCustomerContactInfo;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.DeleteCustomerContactInfo
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/DeleteCustomerContactInfo")]
    public class CustomerContactInfo : IMapFrom<CustomerContactInfoDto>
    {
        public Channel Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactInfo, CustomerContactInfoDto>();
        }
    }
}