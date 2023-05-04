using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift
{
    public class DeleteModel : PageModel
    {
        private readonly IShiftService _shiftService;

        public DeleteModel(IShiftService shiftService) =>
            _shiftService = shiftService;

        public async Task OnPostAsync([FromBody] string[] shifts)
        {
            foreach (var shift in shifts)
            {
                await _shiftService.DeleteShift(shift);
            }
        }
    }
}
