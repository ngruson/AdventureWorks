using AW.Services.SalesPerson.WCF.Messages.GetSalesPerson;
using AW.Services.SalesPerson.WCF.Messages.ListSalesPersons;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.WCF
{
    [ServiceContract]
    public interface ISalesPersonService
    {
        [OperationContract(Action = "ListSalesPersons", ReplyAction = "ListSalesPersons")]
        Task<ListSalesPersonsResponse> ListSalesPersons(ListSalesPersonsRequest request);

        [OperationContract(Action = "GetSalesPerson", ReplyAction = "GetSalesPerson")]
        Task<GetSalesPersonResponse> GetSalesPerson(GetSalesPersonRequest request);
    }
}