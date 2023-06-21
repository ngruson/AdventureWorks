using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Employee;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:EmployeeApiRead")]
public class IndexModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public IndexModel(IEmployeeService employeeService) =>
        _employeeService = employeeService;

    public List<EmployeeViewModel>? Employees { get; set; }
    public async Task OnGetAsync()
    {
        Employees = await _employeeService.GetEmployees();
    }
}
