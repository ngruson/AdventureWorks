using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.AddStoreCustomerContact;
using System.Xml.Serialization;
using AW.Services.Customer.Core.Models.AddStoreCustomerContact;

namespace AW.Services.Customer.WCF.Messages.AddStoreCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class AddStoreCustomerContactRequest : IMapFrom<AddStoreCustomerContactCommand>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContact CustomerContact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddStoreCustomerContactRequest, AddStoreCustomerContactCommand>();
        }
    }
}