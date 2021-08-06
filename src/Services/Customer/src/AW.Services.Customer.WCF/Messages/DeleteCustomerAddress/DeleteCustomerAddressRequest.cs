using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using System.Xml.Serialization;
using AW.Services.Customer.Core.Models.DeleteCustomerAddress;

namespace AW.Services.Customer.WCF.Messages.DeleteCustomerAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteCustomerAddressRequest : IMapFrom<DeleteCustomerAddressCommand>
    {
        public string AccountNumber { get; set; }
        public CustomerAddress CustomerAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCustomerAddressRequest, DeleteCustomerAddressCommand>();
        }
    }
}