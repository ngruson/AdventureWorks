using AW.UI.Web.Admin.Mvc.ViewModels.Department;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> GetDepartments();
    }
}
