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

namespace AW.UI.Web.Store.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> logger;
        private readonly IBasketApiClient basketApiClient;
        private readonly IProductApiClient productApiClient;
        private readonly IMapper mapper;

        public BasketService(
            ILogger<BasketService> logger, 
            IBasketApiClient basketApiClient, 
            IProductApiClient productApiClient,
            IMapper mapper
        ) => 
            (this.logger, this.basketApiClient, this.productApiClient, this.mapper) = (logger, basketApiClient, productApiClient, mapper);

        public async Task<Basket> GetBasketAsync(string userID)
        {
            logger.LogInformation("GetBasket called");
            var dto = await basketApiClient.GetBasket(userID);
            var response = mapper.Map<Basket>(dto);

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
    }
}