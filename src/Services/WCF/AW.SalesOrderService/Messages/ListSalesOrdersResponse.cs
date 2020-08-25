using System.Xml.Serialization;

namespace AW.SalesOrderService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class ListSalesOrdersResponse
    {
        public int TotalSalesOrders { get; set; }
        public ListSalesOrders SalesOrders { get; set; }
    }
}