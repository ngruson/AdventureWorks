using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Department;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
public class DetailModel : PageModel
{
    private readonly IDepartmentService _departmentService;

    public DetailModel(IDepartmentService departmentService) =>
        _departmentService = departmentService;

    [BindProperty]
    public DepartmentViewModel? Department { get; set; }

    public async Task OnGetAsync(Guid objectId)
    {
        Department = await _departmentService.GetDetail(objectId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _departmentService.UpdateDepartment(Department!);
        return Page();
    }

    public async Task<IActionResult> OnGetDeleteAsync(Guid objectId)
    {
        await _departmentService.DeleteDepartment(objectId);

        return RedirectToPage("Index");
    }
}
