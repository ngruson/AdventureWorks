using AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Store.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var item = await productApiClient.GetProductAsync(productNumber);
            var currentBasket = (mapper.Map<Basket>(await basketApiClient.GetBasket(user.Id))) ?? new Basket { BuyerId = user.Id };

            var product = currentBasket.Items.SingleOrDefault(i => i.ProductNumber == item.ProductNumber);
            if (product != null)
            {
                product.Quantity += quantity;
            }
            else
            {
                currentBasket.Items.Add(new BasketItem
                {
                    UnitPrice = item.ListPrice,
                    ThumbnailPhoto = item.ThumbnailPhoto,
                    ProductNumber = item.ProductNumber,
                    ProductName = item.Name,
                    Quantity = quantity,
                    Id = Guid.NewGuid().ToString()
                });
            }

            // Step 5: Update basket
            await basketApiClient.UpdateBasket(mapper.Map<api.Basket>(currentBasket));
            return currentBasket;
        }
    }
}