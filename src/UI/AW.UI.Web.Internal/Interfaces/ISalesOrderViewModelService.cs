using AW.UI.Web.Internal.ViewModels.SalesOrder;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ISalesOrderViewModelService
    {
        Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, string customerType);
        Task<SalesOrderDetailViewModel> GetSalesOrder(string salesOrderNumber);
    }
}