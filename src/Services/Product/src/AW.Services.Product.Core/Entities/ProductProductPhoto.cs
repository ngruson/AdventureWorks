namespace AW.Services.Product.Core.Entities
{
    public class ProductProductPhoto
    {
        private int ProductId { get; set; }
        private int ProductPhotoId { get; set; }

        public bool Primary { get; internal set; }

        public ProductPhoto ProductPhoto { get; private set; }
    }
}