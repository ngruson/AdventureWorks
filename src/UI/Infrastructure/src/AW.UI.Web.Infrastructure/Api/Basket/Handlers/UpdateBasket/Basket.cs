using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.UpdateBasket;

public class Basket : IMapFrom<GetBasket.Basket>
{
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public string? BuyerId { get; set; }

    public decimal Total()
    {
        return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetBasket.Basket, Basket>();
    }
}
