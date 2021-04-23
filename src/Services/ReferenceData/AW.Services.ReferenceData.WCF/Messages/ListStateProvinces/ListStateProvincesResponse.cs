using AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.ReferenceData.WCF.Messages.ListStateProvinces
{
    public class ListStateProvincesResponse
    {
        [XmlArrayItem(ElementName = "StateProvince")]
        public List<StateProvince> StateProvinces { get; set; }
    }
}