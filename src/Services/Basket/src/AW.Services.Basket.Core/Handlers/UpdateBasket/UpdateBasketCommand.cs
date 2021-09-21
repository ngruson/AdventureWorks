using AW.Services.Basket.Core.Model;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.UpdateBasket
{
    public class UpdateBasketCommand : IRequest<CustomerBasket>
    {
        public CustomerBasket Basket { get; set; }
    }
}