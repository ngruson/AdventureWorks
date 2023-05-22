using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public DeleteModel(IEmployeeService employeeService) =>
            _employeeService = employeeService;

        public async Task OnPostAsync([FromBody] Guid[] employees)
        {
            foreach (var employee in employees)
            {
                await _employeeService.DeleteEmployee(employee);
            }
        }
    }
}
