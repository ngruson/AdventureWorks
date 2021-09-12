namespace AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models
{
    public class AddBasketItem
    {
        public string ProductNumber { get; set; }

        public string BasketId { get; set; }

        public int Quantity { get; set; }

        public AddBasketItem()
        {
            Quantity = 1;
        }
    }
}