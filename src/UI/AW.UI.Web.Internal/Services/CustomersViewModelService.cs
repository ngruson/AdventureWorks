using AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.SalesPersonService;
using AW.UI.Web.Internal.SalesTerritoryService;
using AW.UI.Web.Internal.ViewModels;
using AW.UI.Web.Internal.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class CustomersViewModelService : ICustomersViewModelService
    {
        private readonly ILogger<CustomersViewModelService> logger;
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;
        private readonly ISalesTerritoryService salesTerritoryService;
        private readonly ISalesPersonService salesPersonService;

        public CustomersViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ICustomerService customerService,
            ISalesTerritoryService salesTerritoryService,
            ISalesPersonService salesPersonService)
        {
            logger = loggerFactory.CreateLogger<CustomersViewModelService>();
            this.mapper = mapper;
            this.customerService = customerService;
            this.salesTerritoryService = salesTerritoryService;
            this.salesPersonService = salesPersonService;
        }

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, string customerType)
        {
            logger.LogInformation("GetCustomers called");

            var response = await customerService.ListCustomersAsync(
                new ListCustomersRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Territory = territory,
                    CustomerType = !string.IsNullOrEmpty(customerType) ? Enum.Parse<CustomerType>(customerType) : default,
                    CustomerTypeSpecified = !string.IsNullOrEmpty(customerType)
                }
            );

            var vm = new CustomersIndexViewModel
            {
                Customers = mapper.Map<List<CustomerViewModel>>(response.Customers),
                Territories = await GetTerritories(false),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.Customers.Length,
                    TotalItems = response.TotalCustomers,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalCustomers / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }

        public async Task<CustomerDetailViewModel> GetCustomer(string accountNumber)
        {
            logger.LogInformation("GetCustomer called");

            var response = await customerService.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var vm = new CustomerDetailViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
            };

            return vm;
        }

        public async Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetStoreCustomerForEdit called");

            var response = await customerService.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var vm = new EditStoreCustomerViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
                Territories = await GetTerritories(true),
                SalesPersons = await GetSalesPersons(response.Customer.SalesTerritoryName)
            };

            return vm;
        }

        public async Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetIndividualCustomerForEdit called");

            var response = await customerService.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );            

            var vm = new EditIndividualCustomerViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
                Territories = await GetTerritories(true),
                EmailPromotions = GetEmailPromotions()
            };

            return vm;
        }        

        private async Task<IEnumerable<SelectListItem>> GetTerritories(bool edit)
        {
            logger.LogInformation("GetTerritories called.");
            var territories = await salesTerritoryService.ListTerritoriesAsync(
                new ListTerritoriesRequest()
            );

            var items = territories
                .ListTerritoriesResult
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegionCode})" })
                .OrderBy(b => b.Text)
                .ToList();

            if (edit)
                items.Insert(0, new SelectListItem { Value = "", Text = "--Select--", Selected = true });
            else
                items.Insert(0, new SelectListItem { Value = "", Text = "All", Selected = true });

            return items;
        }

        private IEnumerable<SelectListItem> GetEmailPromotions()
        {
            var items = new List<SelectListItem>();

            foreach (EmailPromotionViewModel emailPromotion in (EmailPromotionViewModel[])Enum.GetValues(typeof(EmailPromotionViewModel)))
            {
                items.Add(new SelectListItem(
                        EnumHelper<EmailPromotionViewModel>.GetDisplayValue(emailPromotion),
                        emailPromotion.ToString()
                    )
                );
            }

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private List<SelectListItem> GetCustomerTypes()
        {
            logger.LogInformation("GetCustomerTypes called.");

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "", Text = "All", Selected = true },
                new SelectListItem() { Value = "Individual", Text = "Individual"},
                new SelectListItem() { Value = "Store", Text = "Store" }
            };

            return items;
        }

        private async Task<IEnumerable<SelectListItem>> GetSalesPersons(string salesTerritoryName)
        {
            logger.LogInformation("GetSalesPersons called.");
            var salesPersons = await salesPersonService.ListSalesPersonsAsync(
                new ListSalesPersonsRequest1(new ListSalesPersonsRequest
                {
                    Territory = salesTerritoryName
                })
            );

            var items = salesPersons
                .ListSalesPersonsResult
                .Select(t => new SelectListItem() { Value = t.FullName, Text = t.FullName })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task UpdateStore(CustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateStore called");

            if (!string.IsNullOrEmpty(viewModel.Store.SalesPerson.FullName))
            {
                var salesPersonResponse = await salesPersonService.GetSalesPersonAsync(new GetSalesPersonRequest 
                    { FullName = viewModel.Store.SalesPerson.FullName }
                );

                viewModel.Store.SalesPerson = mapper.Map<SalesPersonViewModel>(salesPersonResponse.SalesPerson);
            }

            logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
            var request = new UpdateCustomerRequest
            {
                Customer = mapper.Map<UpdateCustomer>(viewModel)
            };
                
            logger.LogInformation("Calling UpdateCustomer operation of Customer web service");
            await customerService.UpdateCustomerAsync(request);
            logger.LogInformation("Customer successfully updated");
        }

        public async Task UpdateIndividual(CustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateIndividual called");
            logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");

            var request = new UpdateCustomerRequest
            {
                Customer = mapper.Map<UpdateCustomer>(viewModel)
            };

            logger.LogInformation("Calling UpdateCustomer operation of Customer web service");
            await customerService.UpdateCustomerAsync(request);
            logger.LogInformation("Customer successfully updated");
        }
    }
}