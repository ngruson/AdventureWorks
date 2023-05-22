using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface IEmployeeApiClient
    {
        Task<List<Employee.Handlers.GetEmployees.Employee>?> GetEmployees();
        Task<Employee.Handlers.GetEmployee.Employee?> GetEmployee(Guid objectId);
        Task<List<string>?> GetJobTitles();
        Task<Employee.Handlers.CreateEmployee.Employee?> CreateEmployee(Employee.Handlers.CreateEmployee.Employee employee);
        Task<Employee.Handlers.UpdateEmployee.UpdatedEmployee?> UpdateEmployee(Employee.Handlers.UpdateEmployee.Employee employee);
        Task DeleteEmployee(DeleteEmployeeCommand request);
        Task AddDepartmentHistory(AddDepartmentHistoryCommand command);
        Task UpdateDepartmentHistory(UpdateDepartmentHistoryCommand request);
        Task DeleteDepartmentHistory(DeleteDepartmentHistoryCommand request);
    }
}
