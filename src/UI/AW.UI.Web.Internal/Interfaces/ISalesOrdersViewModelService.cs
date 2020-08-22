using AW.UI.Web.Internal.ViewModels;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ISalesOrdersViewModelService
    {
        Task<SalesOrdersIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, string customerType);
    }
}