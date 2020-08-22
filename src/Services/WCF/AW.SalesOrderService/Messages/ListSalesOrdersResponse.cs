using AW.Application.GetSalesOrders;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.SalesOrderService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class ListSalesOrdersResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesOrderService/1.0/ListSalesOrders")]
        public List<SalesOrderDto> SalesOrder { get; set; } = new List<SalesOrderDto>();
        public int TotalSalesOrders { get; set; }
    }
}