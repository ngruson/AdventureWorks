namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface ISalesPersonApiClient
    {
        Task<List<SalesPerson.Handlers.GetSalesPersons.SalesPerson>?> GetSalesPersonsAsync(string? territory = null);
        Task<SalesPerson.Handlers.GetSalesPerson.SalesPerson?> GetSalesPersonAsync(string firstName, string middleName, string lastName);
    }
}