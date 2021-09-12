using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;

namespace AW.UI.Web.Store.ViewModels
{
    public record Basket : IMapFrom<api.Basket>
    {
        public List<BasketItem> Items { get; init; } = new List<BasketItem>();
        public string BuyerId { get; init; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<api.Basket, Basket>()
                .ReverseMap();
        }
    }
}