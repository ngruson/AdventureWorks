using System.Xml.Serialization;

namespace AW.Services.SalesPerson.WCF.Messages.GetSalesPerson
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesPersonService/1.0", IsNullable = false)]
    public class GetSalesPersonRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}