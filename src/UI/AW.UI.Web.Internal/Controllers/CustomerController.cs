using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.Extensions;

namespace AW.UI.Web.Internal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IReferenceDataService referenceDataService;

        public CustomerController(ICustomerService customerService, IReferenceDataService referenceDataService)
        {
            this.customerService = customerService;
            this.referenceDataService = referenceDataService;
        }

        public async Task<IActionResult> Index(int? pageId, string territoryFilterApplied, CustomerType? customerTypeFilterApplied, string accountNumber)
        {
            return View(
                await customerService.GetCustomers(
                    pageId ?? 0, 
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    customerTypeFilterApplied,
                    accountNumber
                )
            );
        }

        public async Task<IActionResult> Detail(string accountNumber)
        {
            var viewModel = await customerService.GetCustomer(accountNumber);
            ViewData["accountNumber"] = accountNumber;
            ViewData["customerName"] = viewModel.Customer.CustomerName;
            ViewData["countries"] = await referenceDataService.GetCountries();
            ViewData["statesProvinces"] = await referenceDataService.GetStatesProvinces();
            return View(viewModel);
        }

        public async Task<IActionResult> EditStore(string accountNumber)
        {
            return View(
                await customerService.GetStoreCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(EditStoreCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateStore(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditIndividual(string accountNumber)
        {
            return View(
                await customerService.GetIndividualCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditIndividual(EditIndividualCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateIndividual(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        #region Address

        public async Task<IActionResult> AddAddress(string accountNumber, string customerName)
        {
            var vm = customerService.AddAddress(accountNumber, customerName);

            ViewData["addressTypes"] = (await referenceDataService.GetAddressTypes())
                .OrderBy(a => a.Name)
                .ToList()
                .ToSelectList(a => a.Name, a => a.Name);

            ViewData["countries"] = (await referenceDataService.GetCountries())
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(x => x.CountryRegionCode, x => x.Name);

            ViewData["statesProvinces"] = (await referenceDataService.GetStatesProvinces("US"))
                .OrderBy(s => s.Name)
                .ToList()
                .ToSelectList(x => x.StateProvinceCode, x => x.Name);

            return View("Address", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(EditCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.AddAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditAddress(string accountNumber, string addressType)
        {
            var vm = await customerService.GetCustomerAddress(accountNumber, addressType);
            var countryRegionCode = vm.CustomerAddress.Address.CountryRegionCode;

            ViewData["addressTypes"] = (await referenceDataService.GetAddressTypes())
                .OrderBy(a => a.Name)
                .ToList()
                .ToSelectList(a => a.Name, a => a.Name);

            ViewData["countries"] = (await referenceDataService.GetCountries())
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(c => c.CountryRegionCode, c => c.Name);
               
            ViewData["statesProvinces"] = (await referenceDataService.GetStatesProvinces(countryRegionCode))
                .OrderBy(s => s.Name)
                .ToList()
                .ToSelectList(s => s.StateProvinceCode, s => s.Name);

            return View("Address", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(EditCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<JsonResult> GetStatesProvinces(string country)
        {
            var statesProvinces = await customerService.GetStatesProvincesJson(country);
            return Json(statesProvinces);
        }

        public async Task<IActionResult> DeleteAddress(string accountNumber, string addressType)
        {
            var viewModel = await customerService.GetCustomerAddressForDelete(accountNumber, addressType);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(DeleteCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.DeleteAddress(
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
                await customerService.AddContact(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(EditCustomerContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.AddContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditContact(string accountNumber, string contactName, string contactType)
        {
            return View("Contact",
                await customerService.GetCustomerContact(
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
                await customerService.UpdateContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContact(string accountNumber, string contactName, string contactType)
        {
            var viewModel = await customerService.GetCustomerContactForDelete(accountNumber, contactName, contactType);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(DeleteCustomerContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.DeleteContact(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }
        #endregion

        #region Individual contact information

        public IActionResult AddContactEmailAddress(string accountNumber, string customerName)
        {
            return View("ContactInfo",
                customerService.AddEmailAddress(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddContactEmailAddress(EditEmailAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.AddContactEmailAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteIndividualCustomerEmailAddress(string accountNumber, string emailAddress)
        {
            var viewModel = await customerService.GetIndividualCustomerEmailAddressForDelete(accountNumber, emailAddress);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.DeleteIndividualCustomerEmailAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContactEmailAddress(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            var viewModel = await customerService.GetContactEmailAddressForDelete(accountNumber, contactType, contactName, emailAddress);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactEmailAddress(DeleteContactEmailAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.DeleteContactEmailAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        #endregion
    }
}