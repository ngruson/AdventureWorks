using Microsoft.AspNetCore.Mvc;
using AW.UI.Web.Internal.Interfaces;
using System.Threading.Tasks;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;

namespace AW.UI.Web.Internal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerViewModelService customerViewModelService;

        public CustomerController(ICustomerViewModelService customerViewModelService)
        {
            this.customerViewModelService = customerViewModelService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, string customerTypeFilterApplied)
        {
            CustomerType? customerType = null;

            if (customerTypeFilterApplied == "Individual")
                customerType = CustomerType.Individual;
            else if (customerTypeFilterApplied == "Store")
                customerType = CustomerType.Store;

            return View(
                await customerViewModelService.GetCustomers(
                    pageId ?? 0, 
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    customerType
                )
            );
        }

        public async Task<IActionResult> Detail(string accountNumber)
        {
            var viewModel = await customerViewModelService.GetCustomer(accountNumber);
            ViewData["accountNumber"] = accountNumber;
            ViewData["customerName"] = viewModel.Customer.CustomerName;
            return View(viewModel);
        }

        public async Task<IActionResult> EditStore(string accountNumber)
        {
            return View(
                await customerViewModelService.GetStoreCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(EditStoreCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.UpdateStore(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditIndividual(string accountNumber)
        {
            return View(
                await customerViewModelService.GetIndividualCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditIndividual(EditIndividualCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.UpdateIndividual(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        #region Address

        public async Task<IActionResult> AddAddress(string accountNumber, string customerName)
        {
            return View("Address",
                await customerViewModelService.AddAddress(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(EditCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.AddAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditAddress(string accountNumber, string addressType)
        {
            return View("Address",
                await customerViewModelService.GetCustomerAddress(
                    accountNumber, 
                    addressType
                )
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(EditCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.UpdateAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<JsonResult> GetStateProvinces(string country)
        {
            var stateProvinces = await customerViewModelService.GetStateProvincesJson(country);
            return Json(stateProvinces);
        }

        public async Task<IActionResult> DeleteAddress(string accountNumber, string addressType)
        {
            var viewModel = await customerViewModelService.GetCustomerAddressForDelete(accountNumber, addressType);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(DeleteCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.DeleteAddress(
                    viewModel.AccountNumber, 
                    viewModel.AddressType
                );
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }
        #endregion

        #region Store contacts

        public async Task<IActionResult> AddContact(string accountNumber, string customerName)
        {
            return View("Contact",
                await customerViewModelService.AddContact(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(EditCustomerContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.AddContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditContact(string accountNumber, string contactName, string contactType)
        {
            return View("Contact",
                await customerViewModelService.GetCustomerContact(
                    accountNumber,
                    contactName,
                    contactType
                )
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditContact(EditCustomerContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.UpdateContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContact(string accountNumber, string contactName, string contactType)
        {
            var viewModel = await customerViewModelService.GetCustomerContactForDelete(accountNumber, contactName, contactType);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(DeleteCustomerContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.DeleteContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }
        #endregion

        #region Individual contact information

        public IActionResult AddContactEmailAddress(string accountNumber, string customerName)
        {
            return View("ContactInfo",
                customerViewModelService.AddEmailAddress(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddContactEmailAddress(EditEmailAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.AddContactEmailAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteIndividualCustomerEmailAddress(string accountNumber, string emailAddress)
        {
            var viewModel = await customerViewModelService.GetIndividualCustomerEmailAddressForDelete(accountNumber, emailAddress);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerViewModelService.DeleteIndividualCustomerEmailAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContactEmailAddress(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            var viewModel = await customerViewModelService.GetContactEmailAddressForDelete(accountNumber, contactType, contactName, emailAddress);
            return View(viewModel);
        }

        #endregion
    }
}