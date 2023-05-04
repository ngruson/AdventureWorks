using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift
{
    public class DetailModel : PageModel
    {
        private readonly IShiftService _shiftService;

        public DetailModel(IShiftService shiftService) => 
            _shiftService = shiftService;

        [BindProperty]
        public ShiftViewModel? Shift { get; set; }

        public async Task OnGetAsync(string name)
        {
            Shift = await _shiftService.GetDetail(name);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _shiftService.UpdateShift(Shift!);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            await _shiftService.DeleteShift(name);

            return RedirectToPage("Index");
        }
    }
}
