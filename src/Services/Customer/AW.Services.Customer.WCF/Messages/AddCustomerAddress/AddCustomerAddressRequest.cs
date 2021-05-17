using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.AddCustomerAddress;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.AddCustomerAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class AddCustomerAddressRequest : IMapFrom<AddCustomerAddressCommand>
    {
        public string AccountNumber { get; set; }
        public CustomerAddress CustomerAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddCustomerAddressRequest, AddCustomerAddressCommand>();
        }
    }
}