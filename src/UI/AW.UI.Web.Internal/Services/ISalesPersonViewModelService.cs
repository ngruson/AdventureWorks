using AW.UI.Web.Internal.ViewModels.SalesPerson;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ISalesPersonViewModelService
    {
        Task<SalesPersonIndexViewModel> GetSalesPersons();
    }
}