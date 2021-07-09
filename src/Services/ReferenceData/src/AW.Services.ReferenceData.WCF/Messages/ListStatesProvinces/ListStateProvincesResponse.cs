using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.ReferenceData.WCF.Messages.ListStatesProvinces
{
    public class ListStatesProvincesResponse
    {
        [XmlArrayItem(ElementName = "StateProvince")]
        public List<StateProvince> StateProvinces { get; set; }
    }
}