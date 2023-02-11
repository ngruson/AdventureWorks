using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Store.ViewModels.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class BasketCheckout : Basket, IMapFrom<SharedKernel.Basket.Handlers.GetBasket.Basket>
    {
        [Required]
        public string? ShipMethod { get; set; }

        public Address BillToAddress { get; set; } = new();
        public Address ShipToAddress { get; set; } = new();

        [Required]
        [DisplayName("Card number")]
        public string? CardNumber { get; set; }
        [Required]
        [DisplayName("Cardholder name")]
        public string? CardHolderName { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        [CardExpiration(ErrorMessage = "The card is expired"), Required]
        [DisplayName("Card expiration")]
        public string? CardExpirationShort { get; set; }
        [Required]
        [DisplayName("Card security number")]
        public string? CardSecurityNumber { get; set; }
        [Required]
        public string? CardType { get; set; }
        
        [Required]
        public Guid RequestId { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Basket.Handlers.GetBasket.Basket, BasketCheckout>()
                .ForMember(m => m.ShipMethod, opt => opt.Ignore())
                .ForMember(m => m.BillToAddress, opt => opt.Ignore())
                .ForMember(m => m.ShipToAddress, opt => opt.Ignore())
                .ForMember(m => m.CardNumber, opt => opt.Ignore())
                .ForMember(m => m.CardHolderName, opt => opt.Ignore())
                .ForMember(m => m.CardExpirationShort, opt => opt.Ignore())
                .ForMember(m => m.CardSecurityNumber, opt => opt.Ignore())
                .ForMember(m => m.CardType, opt => opt.Ignore())
                .ForMember(m => m.RequestId, opt => opt.Ignore());

            profile.CreateMap<SharedKernel.Basket.Handlers.Checkout.BasketCheckout, BasketCheckout>()
                .ForMember(m => m.CardExpirationShort, opt => opt.Ignore())
                .ForMember(m => m.BuyerId, opt => opt.MapFrom(src => src.Buyer))
                .ForMember(m => m.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap()
                .ForMember(m => m.CardExpiration, opt => opt.MapFrom(src => MapCardExpiration(src.CardExpirationShort!)))
                .ForMember(m => m.CustomerNumber, opt => opt.Ignore());
        }

        private static DateTime MapCardExpiration(string cardExpirationShort)
        {
            return new DateTime(
                int.Parse("20" + cardExpirationShort.Split('/')[1]),
                int.Parse(cardExpirationShort.Split('/')[0]),
                1
            );
        }
    }
}