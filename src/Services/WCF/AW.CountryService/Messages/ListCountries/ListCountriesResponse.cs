using AW.Application.Country.ListCountries;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.CountryService.Messages.ListCountries
{
    public class ListCountriesResponse
    {
        [XmlArrayItem(ElementName = "Country")]
        public List<CountryDto> Countries { get; set; }
    }
}