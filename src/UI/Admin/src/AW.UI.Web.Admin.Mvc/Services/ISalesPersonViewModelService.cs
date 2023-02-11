using AW.UI.Web.Admin.Mvc.ViewModels.SalesPerson;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface ISalesPersonViewModelService
    {
        Task<SalesPersonIndexViewModel> GetSalesPersons(string? territory = null);
    }
}