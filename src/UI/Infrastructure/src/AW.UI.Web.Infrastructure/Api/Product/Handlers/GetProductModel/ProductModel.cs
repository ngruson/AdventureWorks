namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModel
{
    public class ProductModel
    {
        public string? Name { get; set; }

        public string? CatalogDescription { get; set; }

        public string? Instructions { get; set; }
        public List<ProductModelDescription>? Descriptions { get; set; }
    }
}
