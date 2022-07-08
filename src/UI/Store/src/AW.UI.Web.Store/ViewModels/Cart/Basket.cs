using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class Basket : IMapFrom<SharedKernel.Basket.Handlers.GetBasket.Basket>
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        
        public string BuyerId { get; set; }

        public decimal Total => Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Basket.Handlers.GetBasket.Basket, Basket>()
                .ReverseMap();
        }
    }
}