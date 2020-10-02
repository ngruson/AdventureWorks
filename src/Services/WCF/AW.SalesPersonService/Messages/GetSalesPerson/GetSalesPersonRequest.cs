using System.Xml.Serialization;

namespace AW.SalesPersonService.Messages.GetSalesPerson
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesPersonService/1.0", IsNullable = false)]
    public class GetSalesPersonRequest
    {
        public string FullName { get; set; }
    }
}