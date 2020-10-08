using AW.Application.StateProvince;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.StateProvinceService.Messages.ListStateProvinces
{
    public class ListStateProvincesResponse
    {
        [XmlArrayItem(ElementName = "StateProvince")]
        public List<StateProvince> StateProvinces { get; set; }
    }
}