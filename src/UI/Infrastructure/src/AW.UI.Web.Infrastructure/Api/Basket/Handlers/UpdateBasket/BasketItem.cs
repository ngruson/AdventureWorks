using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.UpdateBasket
{
    public class BasketItem : IMapFrom<GetBasket.BasketItem>
    {
        public string? Id { get; init; }
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetBasket.BasketItem, BasketItem>();
        }
    }
}