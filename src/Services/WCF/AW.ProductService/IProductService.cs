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
        Task<ListProductsResponse> ListProducts();
    }

    [MessageContract(IsWrapped = false)]
    public class ListProductsRequestMessage
    {
    }

    [MessageContract(IsWrapped = false)]
    public class ListProductsResponseMessage
    {
        [MessageBodyMember(Namespace = "http://services.aw.com/ProductService/1.0")]
        public ListProductsResponse ListProductsResponse { get; set; }
    }
}