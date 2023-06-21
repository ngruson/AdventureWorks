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
public class DetailStoreModel : PageModel
{
    private readonly ICustomerService _customerService;

    public DetailStoreModel(ICustomerService customerService) =>
        _customerService = customerService;

    [BindProperty]
    public StoreCustomerViewModel? Customer { get; set; }

    [ViewData]
    public List<SelectListItem>? AddressTypes { get; private set; }

    [ViewData]
    public List<SelectListItem>? Countries { get; private set; }

    [ViewData]
    public List<SelectListItem>? StatesProvinces { get; private set; }

    public List<SelectListItem>? Territories { get; private set; }
    public List<SelectListItem>? SalesPersons { get; private set; }

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

        SalesPersons = (await _customerService.GetSalesPersons(Customer!.Territory))
            .ToSelectList(_ => _.Name!.FullName, _ => _.Name!.FullName);
    }

    public async Task OnGetAsync(Guid objectId)
    {
        Customer = await _customerService.GetDetailStore(objectId);

        ViewData["customerId"] = Customer.ObjectId;

        await GetSelectLists();
    }

    public async Task<IActionResult> OnPostAsync([Bind(Prefix = "customer")] StoreCustomerViewModel viewModel)
    {
        Customer = await _customerService.UpdateStore(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAddAddressAsync([Bind(Prefix = "newAddress")] EditCustomerAddressViewModel viewModel)
    {
        Customer = await _customerService.AddAddress<StoreCustomerViewModel>(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAddressAsync([Bind(Prefix = "customerAddress")] EditCustomerAddressViewModel viewModel)
    {
        Customer = await _customerService.UpdateAddress<StoreCustomerViewModel>(viewModel);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAddressAsync(Guid customerId, Guid objectId)
    {
        Customer = await _customerService.DeleteAddress<StoreCustomerViewModel>(customerId, objectId);

        await GetSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnGetGetStatesProvincesAsync([FromRoute] string country)
    {
        return new JsonResult(
            await _customerService.GetStatesProvinces(country)
        );
    }

    public async Task<IActionResult> OnPostDeleteContactAsync(Guid customerId, Guid objectId)
    {
        Customer = await _customerService.DeleteContact(customerId, objectId);

        await GetSelectLists();
        return Page();
    }
}
