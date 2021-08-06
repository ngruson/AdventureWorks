using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.SalesPerson.WCF.Messages.ListSalesPersons
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesPersonService/1.0", IsNullable = false)]
    public class ListSalesPersonsResponse
    {
        public List<Core.Models.SalesPerson> SalesPersons { get; set; } = new List<Core.Models.SalesPerson>();
    }
}