using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:DepartmentApiRead")]
    [Route("/HumanResources/[controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(
            IDepartmentService departmentService
        )
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await _departmentService.GetDepartments()
            );
        }

        public async Task<IActionResult> Detail(string name)
        {
            var department = await _departmentService.GetDetail(
                name
            );

            return View(department);
        }

        public async Task<IActionResult> UpdateDepartment(EditDepartmentViewModel viewModel)
        {
            await _departmentService.UpdateDepartment(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { viewModel.Department!.Name }
            );
        }
    }
}
