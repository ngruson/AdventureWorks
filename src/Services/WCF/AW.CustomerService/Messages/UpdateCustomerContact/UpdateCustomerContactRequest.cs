using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class UpdateCustomerContactRequest
    {
        public string AccountNumber { get; set; }
        public CustomerContact CustomerContact { get; set; }
    }
}