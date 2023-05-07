using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;

namespace AW.UI.Web.Store.Mvc.ViewModels.Cart
{
    public record BasketItem : IMapFrom<Infrastructure.Api.Basket.Handlers.GetBasket.BasketItem>
    {
        public string? Id { get; init; }
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }
        public string ThumbnailPhotoUrl => $"data:image/bmp;base64,{Convert.ToBase64String(ThumbnailPhoto!)}";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Basket.Handlers.GetBasket.BasketItem, BasketItem>()
                .ReverseMap();

            profile.CreateMap<Infrastructure.Api.Basket.Handlers.Checkout.BasketItem, BasketItem>()
                .ReverseMap();
        }
    }
}