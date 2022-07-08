using AutoMapper;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProduct;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetPreferredAddress;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.Basket.Handlers.GetBasket;
using AW.UI.Web.SharedKernel.Basket.Handlers.UpdateBasket;
using AW.UI.Web.SharedKernel.Basket.Handlers.Checkout;

namespace AW.UI.Web.Store.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public BasketService(
            ILogger<BasketService> logger,
            IMediator mediator,
            IMapper mapper
        ) => 
            (this.logger, this.mediator, this.mapper) = 
                (logger, mediator, mapper);

        public async Task<T> GetBasketAsync<T>(string userID)
        {
            logger.LogInformation("GetBasket called");
            var dto = await mediator.Send(new GetBasketQuery(userID));
            var response = mapper.Map<T>(dto);

            return response;
        }

        public async Task<SharedKernel.Basket.Handlers.GetBasket.Basket> AddBasketItemAsync(ApplicationUser user, string productNumber, int quantity)
        {
            logger.LogInformation("Getting product for {ProductNumber}", productNumber);
            var product = await mediator.Send(new GetProductQuery(productNumber));
            Guard.Against.Null(product, nameof(product));

            logger.LogInformation("Getting basket for {UserId}", user.Id);
            var currentBasket = (await mediator.Send(new GetBasketQuery(user.Id))) ?? new SharedKernel.Basket.Handlers.GetBasket.Basket { BuyerId = user.Id };

            var basketItem = currentBasket.Items.SingleOrDefault(i => i.ProductNumber == product.ProductNumber);
            if (basketItem != null)
            {
                logger.LogInformation("Updating basket item with {Quantity}", quantity);
                basketItem.Quantity += quantity;
            }
            else
            {
                basketItem = new SharedKernel.Basket.Handlers.GetBasket.BasketItem
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
            await mediator.Send(
                new UpdateBasketCommand(
                    mapper.Map<SharedKernel.Basket.Handlers.UpdateBasket.Basket>(currentBasket)
                )
            );

            logger.LogInformation("Returning basket");
            return currentBasket;
        }

        public async Task<SharedKernel.Basket.Handlers.GetBasket.Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var currentBasket = await mediator.Send(new GetBasketQuery(user.Id));

            currentBasket.Items.ForEach(item => 
                {
                    if (quantities.ContainsKey(item.Id))
                    {
                        logger.LogInformation("Updating quantity for {BasketItemId}", item.Id);
                        item.Quantity = quantities[item.Id];
                    }
                });

            logger.LogInformation("Updating basket");
            await mediator.Send(
                new UpdateBasketCommand(
                    mapper.Map<SharedKernel.Basket.Handlers.UpdateBasket.Basket>(currentBasket)
                )
            );

            logger.LogInformation("Returning basket");
            return currentBasket;
        }

        public async Task Checkout(SharedKernel.Basket.Handlers.Checkout.BasketCheckout basket, string customerNumber)
        {
            basket.CustomerNumber = customerNumber;

            logger.LogInformation("Checking out basket");
            await mediator.Send(new CheckoutCommand(basket));
        }

        public async Task<CheckoutViewModel> Checkout(ApplicationUser user)
        {
            var vm = new CheckoutViewModel
            {
                Basket = await GetBasketAsync<ViewModels.Cart.BasketCheckout>(user.Id),
                Countries = await GetCountriesAsync(),
                CardTypes = GetCardTypesAsync(),
                ShipMethods = await GetShipMethodsAsync()
            };

            var address = await mediator.Send(new GetPreferredAddressQuery(
                    user.CustomerNumber,
                    "Billing"
                )
            );

            if (address != null)
            {
                logger.LogInformation("Setting billing address");
                vm.Basket.BillToAddress = mapper.Map<ViewModels.Cart.Address>(address);
                vm.StatesProvinces_Billing = await GetStatesProvincesAsync(address.CountryRegionCode);
            }
            else
                vm.StatesProvinces_Billing = await GetStatesProvincesAsync();

            address = await mediator.Send(new GetPreferredAddressQuery(
                    user.CustomerNumber,
                    "Shipping"
                )
            );

            if (address != null)
            {
                logger.LogInformation("Setting shipping address");
                vm.Basket.ShipToAddress = mapper.Map<ViewModels.Cart.Address>(address);
                vm.StatesProvinces_Shipping = await GetStatesProvincesAsync(address.CountryRegionCode);
            }
            else
                vm.StatesProvinces_Shipping = await GetStatesProvincesAsync();

            return vm;
        }

        private async Task<List<SelectListItem>> GetCountriesAsync()
        {
            var countries = await mediator.Send(new GetCountriesQuery());

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
            var statesProvinces = await mediator.Send(new GetStatesProvincesQuery(countryRegionCode));

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
            var shipMethods = await mediator.Send(new GetShipMethodsQuery());

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