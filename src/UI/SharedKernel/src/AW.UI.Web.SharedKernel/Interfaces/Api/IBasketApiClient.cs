namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IBasketApiClient
    {
        Task<Basket.Handlers.GetBasket.Basket> GetBasket(string? userID);
        Task Checkout(Basket.Handlers.Checkout.BasketCheckout basket);
        Task<Basket.Handlers.UpdateBasket.Basket> UpdateBasket(Basket.Handlers.UpdateBasket.Basket? basket);
    }
}