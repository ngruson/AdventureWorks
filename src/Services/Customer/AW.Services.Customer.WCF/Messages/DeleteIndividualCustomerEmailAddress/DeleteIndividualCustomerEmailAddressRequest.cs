using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.DeleteIndividualCustomerEmailAddress;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.DeleteIndividualCustomerEmailAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteIndividualCustomerEmailAddressRequest : IMapFrom<DeleteIndividualCustomerEmailAddressCommand>
    {
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteIndividualCustomerEmailAddressRequest, DeleteIndividualCustomerEmailAddressCommand>();
        }
    }
}