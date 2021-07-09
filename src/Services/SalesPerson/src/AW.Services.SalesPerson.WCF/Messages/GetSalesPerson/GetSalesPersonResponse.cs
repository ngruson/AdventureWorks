using System.Xml.Serialization;

namespace AW.Services.SalesPerson.WCF.Messages.GetSalesPerson
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesPersonService/1.0", IsNullable = false)]
    public class GetSalesPersonResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesPersonService/1.0/GetSalesPerson")]
        public SalesPerson SalesPerson { get; set; } = new SalesPerson();
    }
}