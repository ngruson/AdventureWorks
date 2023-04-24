namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IDepartmentApiClient
    {
        Task<List<Department.Handlers.GetDepartments.Department>?> GetDepartments();
        Task<Department.Handlers.GetDepartment.Department?> GetDepartment(string name);
    }
}
