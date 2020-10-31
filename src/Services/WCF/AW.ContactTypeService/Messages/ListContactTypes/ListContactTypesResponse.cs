using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.ContactTypeService.Messages.ListContactTypes
{
    public class ListContactTypesResponse
    {
        [XmlArrayItem(ElementName = "ContactType")]
        public List<string> ContactTypes { get; set; }
    }
}