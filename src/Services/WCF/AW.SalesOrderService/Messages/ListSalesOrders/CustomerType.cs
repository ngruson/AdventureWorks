using System.Xml.Serialization;

namespace AW.SalesOrderService.Messages.ListSalesOrders
{
    [XmlType(Namespace = "http://services.aw.com/SalesOrderService/1.0/ListSalesOrders")]
    public enum CustomerType
    {
        Individual,
        Store
    }
}