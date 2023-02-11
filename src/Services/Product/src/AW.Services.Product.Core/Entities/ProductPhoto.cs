namespace AW.Services.Product.Core.Entities
{

    public class ProductPhoto
    {
        public int Id { get; set; }
        public byte[]? ThumbNailPhoto { get; private set; }

        public string? ThumbnailPhotoFileName { get; private set; }

        public byte[]? LargePhoto { get; private set; }

        public string? LargePhotoFileName { get; private set; }
    }
}