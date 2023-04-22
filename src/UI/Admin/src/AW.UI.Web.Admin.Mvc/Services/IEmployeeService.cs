using AW.UI.Web.Admin.Mvc.ViewModels.Employee;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();
        Task<EmployeeDetailViewModel> GetDetail(string loginID);
        Task<List<SharedKernel.Department.Handlers.GetDepartments.Department>> GetDepartments();
        Task<List<SharedKernel.Shift.Handlers.GetShifts.Shift>> GetShifts();
        Task<List<string>> GetJobTitles();
        Task UpdateEmployee(EditEmployeeViewModel viewModel);
        Task AddDepartmentHistory(AddDepartmentHistoryViewModel viewModel);
        Task UpdateDepartmentHistory(UpdateDepartmentHistoryViewModel viewModel);
        Task DeleteDepartmentHistory(string loginID, string departmentName, string shiftName, DateTime startDate);
    }
}
