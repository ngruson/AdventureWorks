using AutoMapper;
using AW.SharedKernel.Interfaces;
using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Pages.Customer;

[AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiRead")]
public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;

    public IndexModel(ICustomerService customerService, IMapper mapper) =>
        (_customerService) = (customerService);

    public List<CustomerViewModel>? Customers { get; set; }
    public IEnumerable<SelectListItem>? Territories { get; set; }
    public IEnumerable<SelectListItem>? CustomerTypes { get; set; }
    public string? TerritoryFilterApplied { get; set; }
    public CustomerType? CustomerTypeFilterApplied { get; set; }
    public string? AccountNumber { get; set; }

    public async Task OnGetAsync()
    {
        Customers = await _customerService.GetCustomers();

        Territories = (await _customerService.GetTerritories()).ToSelectList(
            _ => _.ObjectId.ToString(), _ => _.Name);
        CustomerTypes = _customerService.GetCustomerTypes();
    }
}
