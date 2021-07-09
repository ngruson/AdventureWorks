using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.ReferenceData.WCF.Messages.ListContactTypes
{
    public class ListContactTypesResponse
    {
        [XmlArrayItem(ElementName = "ContactType")]
        public List<string> ContactTypes { get; set; }
    }
}