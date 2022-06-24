namespace AW.UI.Web.Store.ViewModels.SalesOrder
{
    public record OrderItem
    {
        public string ProductNumber { get; init; }

        public string ProductName { get; init; }

        public decimal UnitPrice { get; init; }

        public decimal Discount { get; init; }

        public int Units { get; init; }

        public string PictureUrl { get; init; }
    }
}