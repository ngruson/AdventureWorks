using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift
{
    public class CreateModel : PageModel
    {
        private readonly IShiftService _shiftService;

        public CreateModel(IShiftService shiftService) =>
            _shiftService = shiftService;

        [BindProperty]
        public ShiftViewModel? Shift { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _shiftService.CreateShift(Shift!);

            return Page();
        }
    }
}
