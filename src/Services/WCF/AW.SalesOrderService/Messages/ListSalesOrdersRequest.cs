using AW.Domain.Sales;
using System.Xml.Serialization;

namespace AW.SalesOrderService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class ListSalesOrdersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }
        public CustomerType? CustomerType { get; set; }
        [XmlIgnore]
        public bool CustomerTypeSpecified { get; set; }
    }
}