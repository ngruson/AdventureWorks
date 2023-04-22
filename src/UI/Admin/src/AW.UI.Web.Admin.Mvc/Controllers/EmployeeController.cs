using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:EmployeeApiRead")]
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ShiftApiRead")]
    [Route("/HumanResources/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public static readonly string DEPARTMENTS = "departments";
        public static readonly string GENDERS = "genders";
        public static readonly string JOBTITLES = "jobTitles";
        public static readonly string MARITALSTATUSES = "maritalStatuses";
        public static readonly string SHIFTS = "shifts";

        public EmployeeController(
            IEmployeeService employeeService
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

        public async Task<IActionResult> Detail(string loginID)
        {
            var employee = await _employeeService.GetDetail(
                loginID
            );

            ViewData["loginID"] = loginID;

            ViewData[DEPARTMENTS] = (await _employeeService
                .GetDepartments())
                .ToSelectList(_ => _.Name, _ => _.Name);

            ViewData[JOBTITLES] = (await _employeeService
                .GetJobTitles())
                .ToSelectList(_ => _, _ => _);

            ViewData[MARITALSTATUSES] = new List<string>
            {
                "Married",
                "Single"
            }
            .ToSelectList(_ => _, _ => _);

            ViewData[GENDERS] = new List<string>
            {
                "Male",
                "Female"
            }
            .ToSelectList(_ => _, _ => _);

            ViewData[SHIFTS] = (await _employeeService
                .GetShifts())
                .ToSelectList(_ => _.Name, _ => _.Name);

            return View(employee);
        }

        public async Task<IActionResult> UpdateEmployee(EditEmployeeViewModel viewModel)
        {
            await _employeeService.UpdateEmployee(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { loginID = viewModel.Employee!.LoginID }
            );
        }

        public async Task<IActionResult> AddDepartmentHistory([ModelBinder(BinderType = typeof(ViewModelModelBinder<AddDepartmentHistoryViewModel>))] AddDepartmentHistoryViewModel viewModel)
        {
            await _employeeService.AddDepartmentHistory(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { loginID = viewModel.LoginID }
            );
        }

        public async Task<IActionResult> UpdateDepartmentHistory([ModelBinder(BinderType = typeof(ViewModelModelBinder<UpdateDepartmentHistoryViewModel>))] UpdateDepartmentHistoryViewModel viewModel)
        {
            await _employeeService.UpdateDepartmentHistory(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { loginID = viewModel.LoginID }
            );
        }

        public async Task<IActionResult> DeleteDepartmentHistory(
            string loginID, string departmentName, string shiftName, DateTime startDate
        )
        {
            await _employeeService.DeleteDepartmentHistory(loginID, departmentName, shiftName, startDate);

            return RedirectToAction(
                nameof(Detail),
                new { loginID }
            );
        }
    }
}
