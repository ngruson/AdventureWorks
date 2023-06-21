using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Employee;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:EmployeeApiWrite")]
public class CreateModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public CreateModel(IEmployeeService employeeService) =>
        _employeeService = employeeService;

    [BindProperty]
    public EmployeeViewModel? Employee { get; set; }

    public List<SelectListItem>? Genders { get; private set; }
    public List<SelectListItem>? JobTitles { get; private set; }
    public List<SelectListItem>? MaritalStatuses { get; private set; }

    public async Task OnGetAsync()
    {
        await GetSelectLists();
    }

    private async Task GetSelectLists()
    {
        JobTitles = (await _employeeService.GetJobTitles()).ToSelectList(_ => _, _ => _);

        Genders = new List<string>
            {
                "Male",
                "Female"
            }
            .ToSelectList(_ => _, _ => _);

        MaritalStatuses = new List<string>
            {
                "Married",
                "Single"
            }
            .ToSelectList(_ => _, _ => _);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _employeeService.CreateEmployee(Employee!);

        return RedirectToPage("Index");
    }
}
