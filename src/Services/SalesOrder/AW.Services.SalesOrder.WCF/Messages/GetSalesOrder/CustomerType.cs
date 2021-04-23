using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
{
    [XmlType(Namespace = "http://services.aw.com/SalesOrderService/1.0/GetSalesOrder")]
    public enum CustomerType
    {
        Individual,
        Store
    }
}