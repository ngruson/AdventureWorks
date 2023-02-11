using AW.Services.Basket.Core.Models;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.Checkout
{
    public class CheckoutCommand : IRequest<CustomerBasket?>
    {
        public BasketCheckout BasketCheckout { get; set; }

        public CheckoutCommand(BasketCheckout basketCheckout)
        { 
            BasketCheckout = basketCheckout;
        }
    }
}