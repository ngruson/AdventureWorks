using AW.Application.Product.GetProducts;
using System.Collections.Generic;

namespace AW.ProductService.Messages
{
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class ListProductsResponse
    {
        [System.Xml.Serialization.XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/ListProducts")]
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalProducts { get; set; }
    }
}