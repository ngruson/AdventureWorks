namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IBasketApiClient
    {
        Task<Basket.Handlers.GetBasket.Basket> GetBasketAsync(string? userID);
        Task CheckoutAsync(Basket.Handlers.Checkout.BasketCheckout basket);
        Task<Basket.Handlers.UpdateBasket.Basket> UpdateBasketAsync(Basket.Handlers.UpdateBasket.Basket? basket);
    }
}