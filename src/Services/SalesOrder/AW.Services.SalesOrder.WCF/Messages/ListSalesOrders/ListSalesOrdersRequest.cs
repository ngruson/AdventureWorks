using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class ListSalesOrdersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }        
    }
}