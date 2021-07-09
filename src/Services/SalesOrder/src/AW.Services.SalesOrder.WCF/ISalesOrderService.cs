using AW.Services.SalesOrder.WCF.Messages.GetSalesOrder;
using AW.Services.SalesOrder.WCF.Messages.ListSalesOrders;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.WCF
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