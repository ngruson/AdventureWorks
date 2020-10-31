using System.Xml.Serialization;

namespace AW.CustomerService.Messages.DeleteCustomerContact
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class DeleteCustomerContactRequest
    {
        public string AccountNumber { get; set; }
        public string ContactType { get; set; }
        public DeleteContact Contact { get; set; }
    }
}