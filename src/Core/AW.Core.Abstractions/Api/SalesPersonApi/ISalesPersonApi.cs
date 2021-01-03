using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.SalesPersonApi
{
    public interface ISalesPersonApi
    {
        Task<ListSalesPersonsResponse> ListSalesPersonsAsync(ListSalesPersonsRequest request);
        Task<GetSalesPersonResponse> GetSalesPersonAsync(GetSalesPersonRequest request);
    }
}