using AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Store.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> logger;
        private readonly IBasketApiClient basketApiClient;
        private readonly IProductApiClient productApiClient;
        private readonly IReferenceDataService referenceDataService;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public BasketService(
            ILogger<BasketService> logger, 
            IBasketApiClient basketApiClient, 
            IProductApiClient productApiClient,
            IReferenceDataService referenceDataService,
            ICustomerService customerService,
            IMapper mapper
        ) => 
            (this.logger, this.basketApiClient, this.productApiClient, this.referenceDataService, this.customerService, this.mapper) = 
                (logger, basketApiClient, productApiClient, referenceDataService, customerService, mapper);

        public async Task<T> GetBasketAsync<T>(string userID)
        {
            logger.LogInformation("GetBasket called");
            var dto = await basketApiClient.GetBasket(userID);
            var response = mapper.Map<T>(dto);

            return response;
        }

        public async Task<Basket> AddBasketItemAsync(ApplicationUser user, string productNumber, int quantity)
        {
            logger.LogInformation("Getting product for {ProductNumber}", productNumber);
            var product = await productApiClient.GetProductAsync(productNumber);
            Guard.Against.Null(product, nameof(product));

            logger.LogInformation("Getting basket for {UserId}", user.Id);
            var currentBasket = (mapper.Map<Basket>(await basketApiClient.GetBasket(user.Id))) ?? new Basket { BuyerId = user.Id };
            Guard.Against.Null(currentBasket, nameof(currentBasket));

            var basketItem = currentBasket.Items.SingleOrDefault(i => i.ProductNumber == product.ProductNumber);
            if (basketItem != null)
            {
                logger.LogInformation("Updating basket item with {Quantity}", quantity);
                basketItem.Quantity += quantity;
            }
            else
            {
                basketItem = new BasketItem
                {
                    UnitPrice = product.ListPrice,
                    ThumbnailPhoto = product.ThumbnailPhoto,
                    ProductNumber = product.ProductNumber,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Id = Guid.NewGuid().ToString()
                };

                logger.LogInformation("Creating new basket item : {BasketItem}", basketItem);
                currentBasket.Items.Add(basketItem);
            }

            logger.LogInformation("Updating basket");
            await basketApiClient.UpdateBasket(mapper.Map<api.Basket>(currentBasket));

            logger.LogInformation("Returning basket");
            return currentBasket;
        }

        public async Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var currentBasket = mapper.Map<Basket>(await basketApiClient.GetBasket(user.Id));

            currentBasket.Items.ForEach(item => 
                {
                    if (quantities.ContainsKey(item.Id))
                    {
                        logger.LogInformation("Updating quantity for {BasketItemId}", item.Id);
                        item.Quantity = quantities[item.Id];
                    }
                });

            logger.LogInformation("Updating basket");
            await basketApiClient.UpdateBasket(mapper.Map<api.Basket>(currentBasket));

            logger.LogInformation("Returning basket");
            return currentBasket;
        }

        public async Task Checkout(Basket basket, string customerNumber)
        {
            var basketCheckout = mapper.Map<api.BasketCheckout>(basket);
            basketCheckout.CustomerNumber = customerNumber;

            logger.LogInformation("Checking out basket");
            await basketApiClient.Checkout(basketCheckout);
        }

        public async Task<CheckoutViewModel> Checkout(ApplicationUser user)
        {
            var vm = new CheckoutViewModel
            {
                Basket = await GetBasketAsync<BasketCheckout>(user.Id),
                Countries = await GetCountriesAsync(),
                CardTypes = GetCardTypesAsync(),
                ShipMethods = await GetShipMethodsAsync()
            };

            var address = await customerService.GetPreferredAddressAsync(
                user.CustomerNumber, "Billing"
            );
            if (address != null)
            {
                logger.LogInformation("Setting billing address");
                vm.Basket.BillToAddress = mapper.Map<Address>(address);
                vm.StatesProvinces_Billing = await GetStatesProvincesAsync(address.CountryRegionCode);
            }
            else
                vm.StatesProvinces_Billing = await GetStatesProvincesAsync();

            address = await customerService.GetPreferredAddressAsync(
                user.CustomerNumber, "Shipping"
            );
            if (address != null)
            {
                logger.LogInformation("Setting shipping address");
                vm.Basket.ShipToAddress = mapper.Map<Address>(address);
                vm.StatesProvinces_Shipping = await GetStatesProvincesAsync(address.CountryRegionCode);
            }
            else
                vm.StatesProvinces_Shipping = await GetStatesProvincesAsync();

            return vm;
        }

        private async Task<List<SelectListItem>> GetCountriesAsync()
        {
            var countries = await referenceDataService.GetCountriesAsync();

            var items = countries
                .Select(t => new SelectListItem() { Value = t.CountryRegionCode, Text = t.Name })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select country--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private async Task<List<SelectListItem>> GetStatesProvincesAsync(string countryRegionCode = null)
        {
            var statesProvinces = await referenceDataService.GetStatesProvincesAsync(countryRegionCode);

            var items = statesProvinces
                .Select(t => new SelectListItem() { Value = t.StateProvinceCode , Text = t.Name })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select state/province--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private static List<SelectListItem> GetCardTypesAsync()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("--Select card type--", null),
                new SelectListItem("SuperiorCard", "SuperiorCard"),
                new SelectListItem("Vista", "Vista"),
                new SelectListItem("Distinguish", "Distinguish"),
                new SelectListItem("ColonialVoice", "ColonialVoice")
            };
        }

        private async Task<List<SelectListItem>> GetShipMethodsAsync()
        {
            var shipMethods = await referenceDataService.GetShipMethodsAsync();

            var items = shipMethods
                .Select(s => new SelectListItem() { Value = s.Name, Text = s.Name })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select shipping method--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}