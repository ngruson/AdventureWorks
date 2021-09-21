using MediatR;

namespace AW.Services.Basket.Core.Handlers.DeleteBasket
{
    public class DeleteBasketCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}