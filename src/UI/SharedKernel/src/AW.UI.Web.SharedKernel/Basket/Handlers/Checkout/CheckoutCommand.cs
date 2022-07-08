using MediatR;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.Checkout
{
    public class CheckoutCommand : IRequest
    {
        public CheckoutCommand(BasketCheckout? basket)
        {
            Basket = basket;
        }

        public BasketCheckout? Basket { get; init; }
    }
}