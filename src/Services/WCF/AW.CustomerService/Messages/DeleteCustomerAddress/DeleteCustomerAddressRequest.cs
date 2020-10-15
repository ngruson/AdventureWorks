using System.Xml.Serialization;

namespace AW.CustomerService.Messages.DeleteCustomerAddress
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteCustomerAddressRequest
    {
        public string AccountNumber { get; set; }
        public string AddressType { get; set; }
    }
}