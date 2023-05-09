using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Department
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
    public class IndexModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        public IndexModel(IDepartmentService departmentService) =>
            _departmentService = departmentService;

        public List<DepartmentViewModel>? Departments { get; set; }
        public async Task OnGetAsync()
        {
            Departments = await _departmentService.GetDepartments();
        }
    }
}
