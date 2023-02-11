using MediatR;

namespace AW.Services.Basket.Core.Handlers.DeleteBasket
{
    public class DeleteBasketCommand : IRequest<Unit>
    {
        public DeleteBasketCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}