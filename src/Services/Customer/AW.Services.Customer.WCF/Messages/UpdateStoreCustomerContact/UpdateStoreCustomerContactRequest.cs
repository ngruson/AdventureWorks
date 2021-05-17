using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.UpdateStoreCustomerContact;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class UpdateStoreCustomerContactRequest : IMapFrom<UpdateStoreCustomerContactCommand>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContact CustomerContact { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStoreCustomerContactRequest, UpdateStoreCustomerContactCommand>();
        }
    }
}