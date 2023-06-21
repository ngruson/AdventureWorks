using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.Customer;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiRead")]
[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiWrite")]
public class StoreCustomerContactModel : PageModel
{
    private readonly ICustomerService _customerService;

    public StoreCustomerContactModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public StoreCustomerContactViewModel? CustomerContact { get; set; }

    [ViewData]
    public List<SelectListItem>? ContactTypes { get; private set; }

    [ViewData]
    public List<SelectListItem>? PhoneNumberTypes { get; private set; }

    private async Task GetSelectLists()
    {
        ContactTypes = (await _customerService.GetContactTypes())
            .ToSelectList(_ => _.Name, _ => _.Name);

        PhoneNumberTypes = _customerService.GetPhoneNumberTypes()
            .ToSelectList(_ => _, _ => _);
    }

    public async Task OnGetAsync(Guid customerId, string customerName, Guid contactId)
    {
        CustomerContact = await _customerService.GetCustomerContact(customerId, contactId);
        await GetSelectLists();
        ViewData["customerId"] = customerId;
        ViewData["customerName"] = customerName;
    }

    public async Task<IActionResult> OnPostAsync(Guid customerId, string customerName, [ModelBinder(BinderType = typeof(EditStoreCustomerContactViewModelBinder))] EditStoreCustomerContactViewModel viewModel)
    {
        CustomerContact = await _customerService.UpdateContact(customerId, viewModel);

        await GetSelectLists();
        ViewData["customerId"] = customerId;
        ViewData["customerName"] = customerName;

        return Page();
    }

    public IActionResult OnPostDeleteContactEmailAddressAsync()
    {
        return Page();
    }
}
