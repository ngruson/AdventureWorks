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
            ViewData["countries"] = await mediator.Send(new GetCountriesQuery());
            ViewData["statesProvinces"] = await mediator.Send(new GetStatesProvincesQuery("US"));
            ViewData["territories"] = await customerService.GetTerritories(true);
            ViewData["salesPersons"] = await customerService.GetSalesPersons(
                viewModel.Territory
            );

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

            ViewData["addressTypes"] = (await mediator.Send(new GetAddressTypesQuery()))
                .OrderBy(a => a.Name)
                .ToList()
                .ToSelectList(a => a.Name, a => a.Name);

            ViewData["countries"] = (await mediator.Send(new GetCountriesQuery()))
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(x => x.CountryRegionCode, x => x.Name);

            ViewData["statesProvinces"] = (await mediator.Send(new GetStatesProvincesQuery("US")))
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

            ViewData["addressTypes"] = (await mediator.Send(new GetAddressTypesQuery()))
                .OrderBy(a => a.Name)
                .ToList()
                .ToSelectList(a => a.Name, a => a.Name);

            ViewData["countries"] = (await mediator.Send(new GetCountriesQuery()))
                .OrderBy(c => c.Name)
                .ToList()
                .ToSelectList(c => c.CountryRegionCode, c => c.Name);

            ViewData["statesProvinces"] = (await mediator.Send(new GetStatesProvincesQuery(countryRegionCode)))
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

        #endregion
    }
}