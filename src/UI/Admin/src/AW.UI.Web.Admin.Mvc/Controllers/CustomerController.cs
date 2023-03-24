using Microsoft.AspNetCore.Mvc;
using AW.SharedKernel.Interfaces;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.Identity.Web;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:CustomerApiRead")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService
        )
        {
            this.customerService = customerService;
        }

        public async Task<IActionResult> Index(int? pageId, string? territoryFilterApplied, CustomerType? customerType, string? accountNumber)
        {
            return View(
                await customerService.GetCustomers(
                    pageId ?? 0,
                    Constants.ITEMS_PER_PAGE,
                    territoryFilterApplied,
                    customerType,
                    accountNumber
                )
            );
        }

        public async Task<IActionResult?> Detail(string accountNumber)
        {
            var viewModel = await customerService.GetCustomer(accountNumber);
            ViewData["accountNumber"] = accountNumber;
            ViewData["customerName"] = viewModel.CustomerName;
            ViewData["addressTypes"] = await customerService.GetAddressTypes();
            ViewData["countries"] = await customerService.GetCountries();
            ViewData["territories"] = await customerService.GetTerritories();
            ViewData["salesPersons"] = await customerService.GetSalesPersons(
                viewModel.Territory!
            );
            ViewData["statesProvinces"] = await customerService.GetStatesProvinces("US");

            if (viewModel is StoreCustomerViewModel)
                return View("_customerStore", viewModel);
            else if (viewModel is IndividualCustomerViewModel v)
            {
                ViewData["phoneNumberTypes"] = customerService.GetPhoneNumberTypes();
                return View("_customerIndividual", v);
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStore(StoreCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateStore(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIndividual([ModelBinder(BinderType = typeof(IndividualCustomerViewModelBinder))] IndividualCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateIndividual(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        #region Address

        [HttpPost]
        public async Task<IActionResult> AddAddress(CustomerAddressViewModel viewModel, string accountNumber)
        {
            if (ModelState.IsValid)
            {
                await customerService.AddAddress(viewModel, accountNumber);
                return RedirectToAction("Detail", new { accountNumber });
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(CustomerAddressViewModel viewModel, string accountNumber)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateAddress(viewModel, accountNumber);
                return RedirectToAction("Detail", new { accountNumber });
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
            await customerService.DeleteAddress(accountNumber, addressType);
            return RedirectToAction("Detail", new { accountNumber });
        }
        #endregion

        #region Store contacts        

        public async Task<IActionResult> StoreCustomerContact(string? accountNumber, string? contactName) 
        {
            return View(
                await customerService.GetCustomerContact(
                    accountNumber,
                    contactName
                )
            );
        }

        [HttpPost]
        public async Task<IActionResult> StoreCustomerContact([ModelBinder(BinderType = typeof(StoreCustomerContactViewModelBinder))] StoreCustomerContactViewModel viewModel, List<string> email)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateContact(viewModel);
            }

            return RedirectToAction("StoreCustomerContact",
                new { viewModel.AccountNumber, ContactName = viewModel.CustomerContact!.ContactPerson.Name!.FullName }
            );
        }

        public async Task<IActionResult> AddStoreCustomerContact(string? accountNumber, string? customerName)
        {
            return View("StoreCustomerContact",
                await customerService.AddContact(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddStoreCustomerContact(StoreCustomerContactViewModel viewModel, List<string> newEmail)
        {
            if (ModelState.IsValid)
            {
                if (newEmail != null)
                {
                    viewModel.CustomerContact!.ContactPerson.EmailAddresses = new();
                    viewModel.CustomerContact?.ContactPerson.EmailAddresses
                        .AddRange(newEmail.Select(_ => new PersonEmailAddressViewModel { EmailAddress = _ }));
                }

                await customerService.AddContact(viewModel);
                return RedirectToAction("StoreCustomerContact", 
                    new { 
                        viewModel.AccountNumber,
                        ContactName = viewModel.CustomerContact?.ContactPerson.Name!.FullName
                    });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContact(string accountNumber, string contactName)
        {
            if (ModelState.IsValid)
            {
                await customerService.DeleteContact(accountNumber, contactName);                
            }

            return RedirectToAction("Detail", new { accountNumber });
        }
        #endregion

        public async Task<IActionResult> DeleteContactEmailAddress(string accountNumber, string contactName, string emailAddress)
        {
            await customerService.DeleteContactEmailAddress(accountNumber, contactName, emailAddress);

            return RedirectToAction("StoreCustomerContact",
                new
                {
                    accountNumber,
                    contactName
                });
        }

        public async Task<IActionResult> DeleteIndividualCustomerEmailAddress(string accountNumber, string emailAddress)
        {
            await customerService.DeleteIndividualCustomerEmailAddress(accountNumber, emailAddress);

            return RedirectToAction("Detail",
                new
                {
                    accountNumber,
                });
        }
    }
}
