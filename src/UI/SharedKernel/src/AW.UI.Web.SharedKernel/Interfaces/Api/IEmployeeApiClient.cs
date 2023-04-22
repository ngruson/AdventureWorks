using AW.UI.Web.SharedKernel.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateDepartmentHistory;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IEmployeeApiClient
    {
        Task<List<Employee.Handlers.GetEmployees.Employee>?> GetEmployees();
        Task<Employee.Handlers.GetEmployee.Employee?> GetEmployee(string loginID);
        Task<List<string>?> GetJobTitles();
        Task<Employee.Handlers.UpdateEmployee.Employee?> UpdateEmployee(string key, Employee.Handlers.UpdateEmployee.Employee employee);
        Task AddDepartmentHistory(AddDepartmentHistoryCommand command);
        Task UpdateDepartmentHistory(UpdateDepartmentHistoryCommand request);
        Task DeleteDepartmentHistory(DeleteDepartmentHistoryCommand request);
    }
}
