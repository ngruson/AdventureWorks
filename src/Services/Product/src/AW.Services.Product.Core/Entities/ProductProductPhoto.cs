namespace AW.Services.Product.Core.Entities
{
    public class ProductProductPhoto
    {
        public int ProductId { get; set; }
        public int ProductPhotoId { get; set; }

        public bool Primary { get; internal set; }

        public ProductPhoto ProductPhoto { get; private set; }
    }
}