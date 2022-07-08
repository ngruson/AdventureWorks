using MediatR;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.UpdateBasket
{
    public class UpdateBasketCommand : IRequest<Basket>
    {
        public UpdateBasketCommand(Basket? basket)
        {
            Basket = basket;
        }

        public Basket? Basket { get; set; }
    }
}