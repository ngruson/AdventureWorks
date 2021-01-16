using AW.Core.Application.Product.GetProducts;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.ProductService.Messages
{
    public class ListProducts
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/ListProducts")]
        public List<ProductDto> Product { get; set; } = new List<ProductDto>();
    }
}