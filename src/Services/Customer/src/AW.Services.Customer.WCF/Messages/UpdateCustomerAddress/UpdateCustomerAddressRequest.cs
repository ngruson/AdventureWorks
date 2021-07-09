using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomerAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class UpdateCustomerAddressRequest : IMapFrom<UpdateCustomerAddressCommand>
    {
        public string AccountNumber { get; set; }
        public CustomerAddress CustomerAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCustomerAddressRequest, UpdateCustomerAddressCommand>();
        }
    }
}