using AW.SalesPersonService.Messages.GetSalesPerson;
using AW.SalesPersonService.Messages.ListSalesPersons;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesPersonService
{
    [ServiceContract(Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    [XmlSerializerFormat]
    public interface ISalesPersonService
    {
        [OperationContract(Action = "ListSalesPersons", ReplyAction = "ListSalesPersons")]
        Task<ListSalesPersonsResponse> ListSalesPersons(ListSalesPersonsRequest request);

        [OperationContract(Action = "GetSalesPerson", ReplyAction = "GetSalesPerson")]
        Task<GetSalesPersonResponse> GetSalesPerson(GetSalesPersonRequest request);
    }
}