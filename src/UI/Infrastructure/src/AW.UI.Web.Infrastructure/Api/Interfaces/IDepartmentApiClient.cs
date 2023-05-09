using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface IDepartmentApiClient
    {
        Task<List<Department.Handlers.GetDepartments.Department>?> GetDepartments();
        Task<Department.Handlers.GetDepartment.Department?> GetDepartment(Guid objectId);
        Task<Department.Handlers.CreateDepartment.Department?> CreateDepartment(Department.Handlers.CreateDepartment.Department department);
        Task<Department.Handlers.UpdateDepartment.Department?> UpdateDepartment(Department.Handlers.UpdateDepartment.Department department);
        Task DeleteDepartment(DeleteDepartmentCommand request);
    }
}
