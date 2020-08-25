using AW.Application.GetSalesOrders;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.SalesOrderService.Messages
{
    public class ListSalesOrders
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesOrderService/1.0/ListSalesOrders")]
        public List<SalesOrderDto> SalesOrder { get; set; } = new List<SalesOrderDto>();
    }
}