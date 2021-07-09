using System.Xml.Serialization;

namespace AW.Services.Product.WCF.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/ProductService/1.0", IsNullable = false)]
    public class GetProductResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/ProductService/1.0/GetProduct")]
        public Core.Handlers.GetProduct.Product Product { get; set; } = new Core.Handlers.GetProduct.Product();
    }
}