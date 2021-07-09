using System.Xml.Serialization;

namespace AW.Services.Product.WCF.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class ListProductsResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/ListProducts")]
        public ListProducts Products { get; set; } = new ListProducts();
        public int TotalProducts { get; set; }
    }
}