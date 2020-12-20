using AW.Core.Domain.Sales;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.ListCustomers
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class ListCustomersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }
        public CustomerType? CustomerType { get; set; }
        [XmlIgnore]
        public bool CustomerTypeSpecified { get; set; }
    }
}