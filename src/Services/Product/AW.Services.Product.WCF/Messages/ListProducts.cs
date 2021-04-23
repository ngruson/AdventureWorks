using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.Product.WCF.Messages
{
    public class ListProducts
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/ListProducts")]
        public List<Application.GetProducts.Product> Product { get; set; } = new List<Application.GetProducts.Product>();
    }
}