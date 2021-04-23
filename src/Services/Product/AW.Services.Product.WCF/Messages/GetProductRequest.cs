using System.Xml.Serialization;

namespace AW.Services.Product.WCF.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class GetProductRequest
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0")]
        public string ProductNumber { get; set; }
    }
}