namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProducts
{
    public class GetProductsResult
    {
        public List<Product> Products { get; set; } = new();
        public int TotalProducts { get; set; }
    }
}