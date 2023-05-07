namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories
{
    public class ProductCategory
    {
        public string? Name { get; set; }
        public List<ProductSubcategory>? Subcategories { get; set; }
    }
}