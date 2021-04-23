using AW.Services.ReferenceData.Application.CountryRegion.GetCountries;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.ReferenceData.WCF.Messages.ListCountries
{
    public class ListCountriesResponse
    {
        [XmlArrayItem(ElementName = "Country")]
        public List<Country> Countries { get; set; }
    }
}