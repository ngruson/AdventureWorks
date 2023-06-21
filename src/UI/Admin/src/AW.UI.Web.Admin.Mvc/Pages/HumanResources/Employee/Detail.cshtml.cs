using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Employee;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:EmployeeApiRead")]
[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ShiftApiRead")]
public class DetailModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public DetailModel(IEmployeeService employeeService) =>
        _employeeService = employeeService;

    [BindProperty]
    public EmployeeViewModel? Employee { get; set; }

    public List<SelectListItem>? Departments { get; private set; }
    public List<SelectListItem>? Genders { get; private set; }
    public List<SelectListItem>? JobTitles { get; private set; }
    public List<SelectListItem>? MaritalStatuses { get; private set; }
    public List<SelectListItem>? Shifts { get; private set; }

    public async Task OnGetAsync(Guid objectId)
    {
        Employee = await _employeeService.GetDetail(objectId);

        await GetSelectLists();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Employee = await _employeeService.UpdateEmployee(Employee!);

        await GetSelectLists();

        return Page();
    }

    public async Task<IActionResult> OnGetDeleteAsync(Guid objectId)
    {
        await _employeeService.DeleteEmployee(objectId);

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostAddDepartmentHistoryAsync([Bind(Prefix = "departmentHistory")] EditDepartmentHistoryViewModel addDepHist)
    {
        Employee = await _employeeService.AddDepartmentHistory(addDepHist);

        await GetSelectLists();

        return Page();
    }

    public async Task<IActionResult> OnPostUpdateDepartmentHistoryAsync(EditDepartmentHistoryViewModel editDepHist)
    {
        Employee = await _employeeService.UpdateDepartmentHistory(editDepHist);

        await GetSelectLists();

        return Page();
    }

    public async Task<IActionResult> OnGetDeleteDepartmentHistoryAsync(Guid employee, Guid objectId)
    {
        Employee = await _employeeService.DeleteDepartmentHistory(employee, objectId);

        await GetSelectLists();

        return Page();
    }

    private async Task GetSelectLists()
    {
        Departments = (await _employeeService.GetDepartments())
            .ToSelectList(_ => _.ObjectId.ToString(), _ => _.Name);

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

        Shifts = (await _employeeService.GetShifts())
            .ToSelectList(_ => _.ObjectId.ToString(), _ => _.Name);
    }
}
