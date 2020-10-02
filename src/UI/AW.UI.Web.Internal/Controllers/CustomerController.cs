using Microsoft.AspNetCore.Mvc;
using AW.UI.Web.Internal.Interfaces;
using System.Threading.Tasks;
using AW.UI.Web.Internal.ViewModels.Customer;

namespace AW.UI.Web.Internal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersViewModelService customersViewModelService;

        public CustomerController(ICustomersViewModelService customersViewModelService)
        {
            this.customersViewModelService = customersViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            return View(
                await customersViewModelService.GetCustomers(
                    pageId ?? 0, 
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    customerTypeFilterApplied
                )
            );
        }

        public async Task<IActionResult> Detail(string accountNumber)
        {
            return View(
                await customersViewModelService.GetCustomer(
                    accountNumber)
            );
        }

        public async Task<IActionResult> EditStore(string accountNumber)
        {
            return View(
                await customersViewModelService.GetCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(CustomerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customersViewModelService.UpdateStore(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }
    }
}