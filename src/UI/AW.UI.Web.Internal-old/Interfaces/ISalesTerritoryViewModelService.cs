using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ISalesTerritoryViewModelService
    {
        Task<SalesTerritoryIndexViewModel> GetSalesTerritories();
    }
}