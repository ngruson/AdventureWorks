namespace AW.Services.Product.Core.Entities
{
    public class ProductDescription
    {
        public int Id { get; set; }
        
        public string? Description { get; private set; }

        public Guid Rowguid { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public List<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; internal set; } = new();
    }
}