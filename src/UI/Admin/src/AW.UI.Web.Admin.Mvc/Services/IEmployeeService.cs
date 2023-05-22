using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel> GetDetail(Guid objectId);
        Task<List<Department>> GetDepartments();
        Task<List<Shift>> GetShifts();
        Task<List<string>> GetJobTitles();
        Task CreateEmployee(EmployeeViewModel viewModel);
        Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel viewModel);
        Task DeleteEmployee(Guid objectId);

        Task<EmployeeViewModel> AddDepartmentHistory(EditDepartmentHistoryViewModel viewModel);
        Task<EmployeeViewModel> UpdateDepartmentHistory(EditDepartmentHistoryViewModel viewModel);
        Task<EmployeeViewModel> DeleteDepartmentHistory(Guid employee, Guid objectId);
    }
}
