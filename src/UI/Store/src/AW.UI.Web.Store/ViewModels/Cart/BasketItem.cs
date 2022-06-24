using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;

namespace AW.UI.Web.Store.ViewModels
{
    public record BasketItem : IMapFrom<api.BasketItem>
    {
        public string Id { get; init; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
        public string ThumbnailPhotoUrl => $"data:image/bmp;base64,{Convert.ToBase64String(ThumbnailPhoto)}";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<api.BasketItem, BasketItem>()
                .ReverseMap();
        }
    }
}