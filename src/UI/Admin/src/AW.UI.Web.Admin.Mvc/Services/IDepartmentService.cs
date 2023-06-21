using AW.UI.Web.Admin.Mvc.ViewModels.Department;

namespace AW.UI.Web.Admin.Mvc.Services;

public interface IDepartmentService
{
    Task<List<DepartmentViewModel>> GetDepartments();
    Task<DepartmentViewModel> GetDetail(Guid objectId);
    Task CreateDepartment(DepartmentViewModel viewModel);
    Task UpdateDepartment(DepartmentViewModel viewModel);
    Task DeleteDepartment(Guid objectId);
}
