using AW.ProductService.Messages;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.ProductService
{
    [ServiceContract(Namespace = "http://services.aw.com/ProductService/1.0")]
    [XmlSerializerFormat]
    public interface IProductService
    {

        [OperationContract(Action = "ListProducts", ReplyAction = "ListProducts")]
        Task<ListProductsResponse> ListProducts(ListProductsRequest request);

        [OperationContract(Action = "GetProduct", ReplyAction = "GetProduct")]
        Task<GetProductResponse> GetProduct(GetProductRequest request);
    }
}