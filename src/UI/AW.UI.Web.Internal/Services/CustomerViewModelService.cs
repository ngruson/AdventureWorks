using AutoMapper;
using AW.UI.Web.Internal.AddressTypeService;
using AW.UI.Web.Internal.CountryService;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.SalesPersonService;
using AW.UI.Web.Internal.SalesTerritoryService;
using AW.UI.Web.Internal.StateProvinceService;
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
    public class CustomerViewModelService : ICustomerViewModelService
    {
        private readonly ILogger<CustomerViewModelService> logger;
        private readonly IMapper mapper;

        private readonly IAddressTypeService addressTypeService;
        private readonly ICountryService countryService;
        private readonly ICustomerService customerService;
        private readonly ISalesTerritoryService salesTerritoryService;
        private readonly ISalesPersonService salesPersonService;
        private readonly IStateProvinceService stateProvinceService;

        public CustomerViewModelService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IAddressTypeService addressTypeService,
            ICountryService countryService,
            ICustomerService customerService,
            ISalesTerritoryService salesTerritoryService,
            ISalesPersonService salesPersonService,
            IStateProvinceService stateProvinceService)
        {
            logger = loggerFactory.CreateLogger<CustomerViewModelService>();
            this.mapper = mapper;
            this.addressTypeService = addressTypeService;
            this.countryService = countryService;
            this.customerService = customerService;
            this.salesTerritoryService = salesTerritoryService;
            this.salesPersonService = salesPersonService;
            this.stateProvinceService = stateProvinceService;
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
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegion.CountryRegionCode})" })
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

        public async Task<EditCustomerAddressViewModel> AddAddress(string accountNumber, string customerName)
        {
            logger.LogInformation("AddAddress called");

            var vm = new EditCustomerAddressViewModel
            {
                IsNewAddress = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerAddressViewModel = new CustomerAddressViewModel {  
                    Address = new AddressViewModel
                    {
                        StateProvince = new StateProvinceViewModel
                        {
                            CountryRegion = new CountryRegionViewModel
                            {
                                CountryRegionCode = "US"
                            }
                        }
                    }
                },
                AddressTypes = await GetAddressTypes(),
                Countries = await GetCountries(),
                StateProvinces = await GetStateProvinces("US")
            };

            return vm;
        }

        public async Task AddAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("AddAddress called");

            var request = new AddCustomerAddressRequest
            {
                CustomerAddress = mapper.Map<CustomerAddress2>(viewModel)
            };

            logger.LogInformation("Calling AddCustomerAddress operation of Customer web service");
            await customerService.AddCustomerAddressAsync(request);
            logger.LogInformation("Address successfully added");
        }

        private async Task<IEnumerable<SelectListItem>> GetAddressTypes()
        {
            logger.LogInformation("GetAddressTypes called.");
            var addressTypes = await addressTypeService.ListAddressTypesAsync();

            var items = addressTypes
                .AddressTypes
                .Select(at => new SelectListItem() { Value = at, Text = at })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            logger.LogInformation("GetCountries called.");
            var response = await countryService.ListCountriesAsync();

            var items = response     
                .Countries
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.CountryRegionCode, Text = c.Name })                
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("GetCustomerAddress called");

            var response = await customerService.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var addresses = response.Customer.Store != null ? response.Customer.Store.Addresses :
                response.Customer.Person.Addresses;

            var address = addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = new EditCustomerAddressViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = response.Customer.Store != null ? response.Customer.Store.Name : response.Customer.Person.FullName,
                CustomerAddressViewModel = mapper.Map<CustomerAddressViewModel>(address),
                AddressTypes = await GetAddressTypes(),
                Countries = await GetCountries(),
                StateProvinces = await GetStateProvinces(address.Address.StateProvince.CountryRegion.CountryRegionCode)
            };

            return vm;
        }

        public async Task UpdateAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("EditAddress called");

            var request = mapper.Map<UpdateCustomerAddressRequest>(viewModel);

            logger.LogInformation("Calling UpdateCustomerAddress operation of Customer web service");
            await customerService.UpdateCustomerAddressAsync(request);
            logger.LogInformation("Address successfully updated");
        }

        public async Task<IEnumerable<SelectListItem>> GetStateProvinces(string country)
        {
            logger.LogInformation("GetStateProvinces called.");
            var response = await stateProvinceService.ListStateProvincesAsync(new ListStateProvincesRequest
            {
                CountryRegionCode = country
            });

            var items = response
                .StateProvinces
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.StateProvinceCode, Text = c.Name })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<DeleteCustomerAddressViewModel> GetCustomerAddressForDelete(string accountNumber, string addressType)
        {
            logger.LogInformation("GetCustomerAddressForDelete called");

            var response = await customerService.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var addresses = response.Customer.Store != null ? response.Customer.Store.Addresses :
                response.Customer.Person.Addresses;

            var address = addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = mapper.Map<DeleteCustomerAddressViewModel>(address);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = response.Customer.Store != null ? response.Customer.Store.Name : response.Customer.Person.FullName;

            return vm;
        }

        public async Task<IEnumerable<StateProvinceViewModel>> GetStateProvincesJson(string country)
        {
            var response = await stateProvinceService.ListStateProvincesAsync(new ListStateProvincesRequest
            {
                CountryRegionCode = country
            });

            return mapper.Map<IEnumerable<StateProvinceViewModel>>(response.StateProvinces);
        }

        public async Task DeleteAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("DeleteAddress called");

            logger.LogInformation("Calling DeleteCustomerAddress operation of Customer web service");
            await customerService.DeleteCustomerAddressAsync(new DeleteCustomerAddressRequest
            {
                AccountNumber = accountNumber,
                AddressType = addressType
            });
            logger.LogInformation("Address successfully deleted");
        }
    }
}