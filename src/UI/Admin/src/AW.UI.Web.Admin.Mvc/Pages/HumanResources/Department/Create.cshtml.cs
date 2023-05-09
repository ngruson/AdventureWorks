using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Department
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
    public class CreateModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        public CreateModel(IDepartmentService departmentService) =>
            _departmentService = departmentService;

        [BindProperty]
        public DepartmentViewModel? Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _departmentService.CreateDepartment(Department!);

            return RedirectToPage("Index");
        }
    }
}
