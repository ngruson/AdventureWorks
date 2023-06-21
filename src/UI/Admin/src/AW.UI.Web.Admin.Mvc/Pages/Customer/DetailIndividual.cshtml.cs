using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.Customer;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiRead")]
[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiWrite")]
public class DetailIndividualModel : PageModel
{
    private readonly ICustomerService _customerService;

    public DetailIndividualModel(ICustomerService customerService) =>
        _customerService = customerService;

    [BindProperty]
    public IndividualCustomerViewModel? Customer { get; set; }

    [ViewData]
    public List<SelectListItem>? AddressTypes { get; private set; }

    [ViewData]
    public List<SelectListItem>? Countries { get; private set; }

    [ViewData]
    public List<SelectListItem>? StatesProvinces { get; private set; }

    [ViewData]
    public List<SelectListItem>? Territories { get; private set; }

    private async Task GetSelectLists()
    {
        AddressTypes = (await _customerService.GetAddressTypes())
            .ToSelectList(_ => _.Name, _ => _.Name);

        Countries = (await _customerService.GetCountries())
            .ToSelectList(_ => _.CountryRegionCode, _ => _.Name);

        StatesProvinces = (await _customerService.GetStatesProvinces("US"))
            .ToSelectList(_ => _.StateProvinceCode, _ => _.Name);

        Territories = (await _customerService.GetTerritories())
            .ToSelectList(_ => _.Name, _ => _.Name);
    }

    public async Task OnGetAsync(Guid objectId)
    {
        Customer = await _customerService.GetDetailIndividual(objectId);

        ViewData["customerId"] = Customer.ObjectId;

        await GetSelectLists();
    }

    public async Task<IActionResult> OnPostAsync([Bind(Prefix = "customer")] IndividualCustomerViewModel viewModel)
    {
        Customer = await _customerService.UpdateIndividual(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAddAddressAsync([Bind(Prefix = "newAddress")] EditCustomerAddressViewModel viewModel)
    {
        Customer = await _customerService.AddAddress<IndividualCustomerViewModel>(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAddressAsync([Bind(Prefix = "customerAddress")] EditCustomerAddressViewModel viewModel)
    {
        Customer = await _customerService.UpdateAddress<IndividualCustomerViewModel>(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAddressAsync(Guid customerId, Guid objectId)
    {
        Customer = await _customerService.DeleteAddress<IndividualCustomerViewModel>(customerId, objectId);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnGetGetStatesProvincesAsync(string country)
    {
        return new JsonResult(
            await _customerService.GetStatesProvinces(country)
        );
    }
}
