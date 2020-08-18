using System.Xml.Serialization;

namespace AW.ProductService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class ListProductsRequest
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0")]
        public int PageIndex { get; set; }

        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0")]
        public int PageSize { get; set; }
        
    }
}