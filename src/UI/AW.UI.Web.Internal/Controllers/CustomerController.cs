﻿using Microsoft.AspNetCore.Mvc;
using AW.UI.Web.Internal.Interfaces;
using System.Threading.Tasks;
using AW.UI.Web.Internal.ViewModels.Customer;
using System.Linq;

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
            var viewModel = await customersViewModelService.GetCustomer(accountNumber);
            ViewData["accountNumber"] = accountNumber;
            ViewData["customerName"] = viewModel.Customer.Name;
            return View(viewModel);
        }

        public async Task<IActionResult> EditStore(string accountNumber)
        {
            return View(
                await customersViewModelService.GetStoreCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(EditStoreCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customersViewModelService.UpdateStore(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditIndividual(string accountNumber)
        {
            return View(
                await customersViewModelService.GetIndividualCustomerForEdit(
                    accountNumber)
            );
        }

        [HttpPost]
        public async Task<IActionResult> EditIndividual(EditIndividualCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customersViewModelService.UpdateIndividual(viewModel.Customer);
                return RedirectToAction("Detail", new { viewModel.Customer.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> AddAddress(string accountNumber, string customerName)
        {
            return View("Address",
                await customersViewModelService.AddAddress(accountNumber, customerName)
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(EditCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customersViewModelService.AddAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditAddress(string accountNumber, string addressType)
        {
            return View("Address",
                await customersViewModelService.GetCustomerAddress(
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
                await customersViewModelService.UpdateAddress(viewModel);
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }

        public async Task<JsonResult> GetStateProvinces(string country)
        {
            var stateProvinces = await customersViewModelService.GetStateProvincesJson(country);
            return Json(stateProvinces);
        }

        public async Task<IActionResult> DeleteAddress(string accountNumber, string addressType)
        {
            var viewModel = await customersViewModelService.GetCustomerAddressForDelete(accountNumber, addressType);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(DeleteCustomerAddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await customersViewModelService.DeleteAddress(
                    viewModel.AccountNumber, 
                    viewModel.AddressType
                );
                return RedirectToAction("Detail", new { viewModel.AccountNumber });
            }

            return View(viewModel);
        }
    }
}