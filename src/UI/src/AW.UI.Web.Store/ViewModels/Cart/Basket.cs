using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Store.ViewModels.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class Basket : IMapFrom<api.Basket>
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        
        [Required]
        public string ShipMethod { get; set; }

        public Address BillToAddress { get; set; } = new();
        public Address ShipToAddress { get; set; } = new();

        [Required]
        [DisplayName("Card number")]
        public string CardNumber { get; set; }
        [Required]
        [DisplayName("Cardholder name")]
        public string CardHolderName { get; set; }
        
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        [CardExpiration(ErrorMessage = "The card is expired"), Required]
        [DisplayName("Card expiration")]
        public string CardExpirationShort { get; set; }
        [Required]
        [DisplayName("Card security number")]
        public string CardSecurityNumber { get; set; }
        [Required]
        public string CardType { get; set; }

        public string BuyerId { get; set; }
        [Required]
        public Guid RequestId { get; set; }

        public decimal Total => Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);

        public void Mapping(Profile profile)
        {
            profile.CreateMap<api.Basket, Basket>()
                .ReverseMap();

            profile.CreateMap<Basket, api.BasketCheckout>()
                .ForMember(m => m.Buyer, opt => opt.MapFrom(src => src.BuyerId))
                .ForMember(m => m.CardExpiration, opt => opt.MapFrom(src => MapCardExpiration(src.CardExpirationShort)));
        }

        private DateTime MapCardExpiration(string cardExpirationShort)
        {
            return new DateTime(
                int.Parse("20" + cardExpirationShort.Split('/')[1]),
                int.Parse(cardExpirationShort.Split('/')[0]),
                1
            );
        }
    }
}