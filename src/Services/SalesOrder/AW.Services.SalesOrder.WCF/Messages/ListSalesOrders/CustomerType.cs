using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    [XmlType(Namespace = "http://services.aw.com/SalesOrderService/1.0/ListSalesOrders")]
    public enum CustomerType
    {
        Individual,
        Store
    }
}