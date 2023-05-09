using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Department
{
    public class DeleteModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        public DeleteModel(IDepartmentService departmentService) =>
            _departmentService = departmentService;

        public async Task OnPostAsync([FromBody] Guid[] departments)
        {
            foreach (var department in departments)
            {
                await _departmentService.DeleteDepartment(department);
            }
        }
    }
}
