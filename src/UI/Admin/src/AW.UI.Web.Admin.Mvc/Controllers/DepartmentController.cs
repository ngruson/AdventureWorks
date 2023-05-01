using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
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

        public IActionResult CreateDepartment()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([ModelBinder(BinderType = typeof(ViewModelModelBinder<CreateDepartmentViewModel>))]  CreateDepartmentViewModel viewModel)
        {
            await _departmentService.CreateDepartment(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { viewModel.Department!.Name }
            );
        }

        public async Task<IActionResult> UpdateDepartment(EditDepartmentViewModel viewModel)
        {
            await _departmentService.UpdateDepartment(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { viewModel.Department!.Name }
            );
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartments([FromBody] string[] departments)
        {
            foreach (var department in departments)
            {
                await _departmentService.DeleteDepartment(department);
            }

            return new OkResult();
        }

        public async Task<IActionResult> DeleteDepartment(string name)
        {
            await _departmentService.DeleteDepartment(name);

            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}
