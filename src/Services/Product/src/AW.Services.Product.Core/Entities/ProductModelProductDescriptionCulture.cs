namespace AW.Services.Product.Core.Entities
{
    public class ProductModelProductDescriptionCulture
    {
        public int ProductModelID { get; set; }
        public int ProductDescriptionID { get; set; }

        public string? CultureID { get; set; }

        public ProductDescription? ProductDescription { get; set; }
        public Culture? Culture { get; set; }
    }
}
