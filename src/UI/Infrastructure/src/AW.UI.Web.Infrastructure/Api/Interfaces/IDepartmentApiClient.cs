using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface IDepartmentApiClient
    {
        Task<List<Department.Handlers.GetDepartments.Department>?> GetDepartments();
        Task<Department.Handlers.GetDepartment.Department?> GetDepartment(string name);
        Task<Department.Handlers.CreateDepartment.Department?> CreateDepartment(Department.Handlers.CreateDepartment.Department department);
        Task<Department.Handlers.UpdateDepartment.Department?> UpdateDepartment(UpdateDepartmentCommand command);
        Task DeleteDepartment(DeleteDepartmentCommand request);
    }
}
