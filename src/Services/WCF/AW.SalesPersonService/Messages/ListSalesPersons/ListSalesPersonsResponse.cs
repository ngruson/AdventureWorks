using AW.Application.SalesPerson;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.SalesPersonService.Messages.ListSalesPersons
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesPersonService/1.0", IsNullable = false)]
    public class ListSalesPersonsResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesPersonService/1.0/ListSalesPersons")]
        public List<SalesPersonDto> SalesPersons { get; set; } = new List<SalesPersonDto>();
    }
}