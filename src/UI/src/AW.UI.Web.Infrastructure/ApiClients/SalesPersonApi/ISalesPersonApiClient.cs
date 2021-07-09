using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi
{
    public interface ISalesPersonApiClient
    {
        Task<List<Models.SalesPerson>> GetSalesPersonsAsync(string territory = null);
        Task<Models.SalesPerson> GetSalesPersonAsync(string firstName, string middleName, string lastName);
    }
}