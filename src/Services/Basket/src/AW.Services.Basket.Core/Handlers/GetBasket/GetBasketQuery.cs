using AW.Services.Basket.Core.Model;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.GetBasket
{
    public class GetBasketQuery : IRequest<CustomerBasket>
    {
        public string Id { get; set; }
    }
}