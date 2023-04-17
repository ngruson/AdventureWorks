using AW.UI.Web.Admin.Mvc.ViewModels.Employee;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();
    }
}
