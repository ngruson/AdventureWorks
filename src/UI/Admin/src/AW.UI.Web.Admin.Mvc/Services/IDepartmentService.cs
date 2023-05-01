using AW.UI.Web.Admin.Mvc.ViewModels.Department;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> GetDepartments();
        Task<DepartmentDetailViewModel> GetDetail(string name);
        Task CreateDepartment(CreateDepartmentViewModel viewModel);
        Task UpdateDepartment(EditDepartmentViewModel viewModel);
        Task DeleteDepartment(string department);
    }
}
