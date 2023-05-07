using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();
        Task<EmployeeDetailViewModel> GetDetail(string loginID);
        Task<List<Department>> GetDepartments();
        Task<List<Shift>> GetShifts();
        Task<List<string>> GetJobTitles();
        Task UpdateEmployee(EditEmployeeViewModel viewModel);
        Task AddDepartmentHistory(AddDepartmentHistoryViewModel viewModel);
        Task UpdateDepartmentHistory(UpdateDepartmentHistoryViewModel viewModel);
        Task DeleteDepartmentHistory(string loginID, string departmentName, string shiftName, DateTime startDate);
    }
}
