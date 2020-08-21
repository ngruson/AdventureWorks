using AW.Application.GetProduct;
using System.Xml.Serialization;

namespace AW.ProductService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class GetProductResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/GetProduct")]
        public ProductDto Product { get; set; } = new ProductDto();
    }
}