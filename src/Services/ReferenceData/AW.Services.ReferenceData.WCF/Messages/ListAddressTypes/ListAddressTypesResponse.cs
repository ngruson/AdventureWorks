using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.ReferenceData.WCF.Messages.ListAddressTypes
{
    public class ListAddressTypesResponse
    {
        [XmlArrayItem(ElementName = "AddressType")]
        public List<string> AddressTypes { get; set; }
    }
}