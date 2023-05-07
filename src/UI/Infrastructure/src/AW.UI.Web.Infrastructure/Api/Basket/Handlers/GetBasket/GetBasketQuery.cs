using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.GetBasket
{
    public class GetBasketQuery : IRequest<Basket>
    {
        public GetBasketQuery(string? userID)
        {
            UserID = userID;
        }

        public string? UserID { get; init; }
    }
}