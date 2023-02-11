using AW.Services.Basket.Core.Models;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.UpdateBasket
{
    public class UpdateBasketCommand : IRequest<CustomerBasket>
    {
        public CustomerBasket Basket { get; private init; }

        public UpdateBasketCommand(CustomerBasket basket)
        {
            Basket = basket;
        }
    }
}