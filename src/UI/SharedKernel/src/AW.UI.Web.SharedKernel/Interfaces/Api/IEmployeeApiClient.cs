using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IEmployeeApiClient
    {
        Task<List<Employee.Handlers.GetEmployees.Employee>?> GetEmployees();
    }
}
