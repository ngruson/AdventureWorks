using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string BuyerId { get; set; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }
    }
}