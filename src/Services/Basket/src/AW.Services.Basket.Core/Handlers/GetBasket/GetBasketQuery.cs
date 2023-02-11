using AW.Services.Basket.Core.Models;
using MediatR;

namespace AW.Services.Basket.Core.Handlers.GetBasket
{
    public class GetBasketQuery : IRequest<CustomerBasket?>
    {
        public string Id { get; set; }

        public GetBasketQuery(string id)
        {
            Id = id;
        }
    }
}