namespace AW.Services.Product.Core.Entities
{

    public class ProductPhoto
    {
        public int Id { get; set; }
        public byte[] ThumbNailPhoto { get; set; }

        public string ThumbnailPhotoFileName { get; set; }

        public byte[] LargePhoto { get; set; }

        public string LargePhotoFileName { get; set; }
    }
}