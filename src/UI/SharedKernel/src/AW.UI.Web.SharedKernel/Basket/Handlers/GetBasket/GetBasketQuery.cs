using MediatR;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.GetBasket
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