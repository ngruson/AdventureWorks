using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AW.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Admin.Mvc;
using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using System.Collections.Generic;
using AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IMediator mediator;

        public CustomerController(ICustomerService customerService, IMediator mediator
        )
        {
            this.customerService = customerService;
            this.mediator = mediator;
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
            ViewData["customerName"] = viewModel.CustomerName;
            ViewData["addressTypes"] = await customerService.GetAddressTypes();
            ViewData["countries"] = await customerService.GetCountries();
            ViewData["territories"] = await customerService.GetTerritories();
            ViewData["salesPersons"] = await customerService.GetSalesPersons(
                viewModel.Territory
            );
            ViewData["statesProvinces"] = await customerService.GetStatesProvinces("US");

            return View("_customerStore", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(StoreCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateStore(viewModel);
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

        public async Task<IActionResult> StoreCustomerContact(string accountNumber, string contactName) 
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
                new { viewModel.AccountNumber, ContactName = viewModel.CustomerContact.ContactPerson.Name.FullName }
            );
        }

        public async Task<IActionResult> AddStoreCustomerContact(string accountNumber, string customerName)
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
                    viewModel.CustomerContact.ContactPerson.EmailAddresses = new();
                    viewModel.CustomerContact.ContactPerson.EmailAddresses
                        .AddRange(newEmail.Select(_ => new PersonEmailAddressViewModel { EmailAddress = _ }));
                }

                await customerService.AddContact(viewModel);
                return RedirectToAction("StoreCustomerContact", 
                    new { 
                        viewModel.AccountNumber,
                        ContactName = viewModel.CustomerContact.ContactPerson.Name.FullName
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
    }
}