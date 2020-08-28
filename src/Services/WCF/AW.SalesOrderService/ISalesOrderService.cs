using AW.SalesOrderService.Messages.GetSalesOrder;
using AW.SalesOrderService.Messages.ListSalesOrders;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesOrderService
{
    [ServiceContract(Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlSerializerFormat]
    public interface ISalesOrderService
    {
        [OperationContract(Action = "ListSalesOrders", ReplyAction = "ListSalesOrders")]
        Task<ListSalesOrdersResponse> ListSalesOrders(ListSalesOrdersRequest request);

        [OperationContract(Action = "GetSalesOrder", ReplyAction = "GetSalesOrder")]
        Task<GetSalesOrderResponse> GetSalesOrder(GetSalesOrderRequest request);
    }
}