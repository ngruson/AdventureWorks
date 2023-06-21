using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ShiftApiRead")]
public class IndexModel : PageModel
{
    private readonly IShiftService _shiftService;

    public IndexModel(IShiftService shiftService) =>
        _shiftService = shiftService;

    public List<ShiftViewModel>? Shifts { get; set; }
    public async Task OnGetAsync()
    {
        Shifts = await _shiftService.GetShifts();
    }
}
