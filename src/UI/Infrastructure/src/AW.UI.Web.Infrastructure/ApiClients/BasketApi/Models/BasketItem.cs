namespace AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models
{
    public class BasketItem
    {
        public string Id { get; init; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
    }
}