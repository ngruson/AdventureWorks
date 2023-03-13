namespace AW.Services.Product.Core.Entities
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; private set; }

        public string? CatalogDescription { get; private set; }

        public string? Instructions { get; private set; }

        public List<ProductModelIllustration> ProductModelIllustrations { get; internal set; } = new();

        public List<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; internal set; } = new();
    }
}
