namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface ISalesPersonApiClient
    {
        Task<List<SalesPerson.Handlers.GetSalesPersons.SalesPerson>?> GetSalesPersonsAsync(string? territory = null);
        Task<SalesPerson.Handlers.GetSalesPerson.SalesPerson?> GetSalesPersonAsync(string firstName, string middleName, string lastName);
    }
}