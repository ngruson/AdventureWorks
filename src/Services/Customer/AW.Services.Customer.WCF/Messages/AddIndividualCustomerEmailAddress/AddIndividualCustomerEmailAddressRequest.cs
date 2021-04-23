using AutoMapper;
using AW.Services.Customer.Application.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.Application.Common;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.AddIndividualCustomerEmailAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class AddIndividualCustomerEmailAddressRequest : IMapFrom<AddIndividualCustomerEmailAddressCommand>
    {
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddIndividualCustomerEmailAddressRequest, AddIndividualCustomerEmailAddressCommand>();
        }
    }
}