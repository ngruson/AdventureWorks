using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:EmployeeApiRead")]
    [Route("HumanResources/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService
        )
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await _employeeService.GetEmployees()
            );
        }
    }
}
