using AW.Services.Basket.Core.Models;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.Checkout
{
    public class CheckoutCommand : IRequest<CustomerBasket>
    {
        public string UserName { get; set; }
        public BasketCheckout BasketCheckout { get; set; }
    }
}