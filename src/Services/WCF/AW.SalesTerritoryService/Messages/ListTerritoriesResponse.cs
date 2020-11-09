using AW.Application.SalesTerritory.GetSalesTerritories;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.SalesTerritoryService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesTerritoryService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesTerritoryService/1.0", IsNullable = false)]
    public class ListTerritoriesResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesTerritoryService/1.0/ListTerritories")]
        public List<TerritoryDto> Territories { get; set; } = new List<TerritoryDto>();
    }
}