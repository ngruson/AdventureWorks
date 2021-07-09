namespace AW.Services.Product.Core.Entities
{
    public class ProductProductPhoto
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductPhotoId { get; set; }

        public bool Primary { get; set; }

        public virtual ProductPhoto ProductPhoto { get; set; }
    }
}