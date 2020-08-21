using System.Xml.Serialization;

namespace AW.CustomerService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class ListCustomersRequest
    {
        [XmlElement(Namespace = "http://services.aw.com/CustomerService/1.0")]
        public int PageIndex { get; set; }

        [XmlElement(Namespace = "http://services.aw.com/CustomerService/1.0")]
        public int PageSize { get; set; }

        public string Territory { get; set; }

    }
}