using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class GetSalesOrderResponse
    {
        public SalesOrder SalesOrder { get; set; } = new SalesOrder();
    }
}