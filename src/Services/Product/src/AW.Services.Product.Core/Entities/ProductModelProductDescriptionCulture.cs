namespace AW.Services.Product.Core.Entities
{
    public class ProductModelProductDescriptionCulture
    {
        public int ProductModelID { get; set; }
        public int ProductDescriptionID { get; set; }

        public string? CultureID { get; set; }

        public ProductDescription? ProductDescription { get; private set; }
        public Culture? Culture { get; private set; }
    }
}
