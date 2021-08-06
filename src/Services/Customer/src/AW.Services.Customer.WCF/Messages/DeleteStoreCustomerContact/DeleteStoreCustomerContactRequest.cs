using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using System.Xml.Serialization;
using AW.Services.Customer.Core.Models.DeleteStoreCustomerContact;

namespace AW.Services.Customer.WCF.Messages.DeleteStoreCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteStoreCustomerContactRequest : IMapFrom<DeleteStoreCustomerContactCommand>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContact CustomerContact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteStoreCustomerContactRequest, DeleteStoreCustomerContactCommand>();
        }
    }
}