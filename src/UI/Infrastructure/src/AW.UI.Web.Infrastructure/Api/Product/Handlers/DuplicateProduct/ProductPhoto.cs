using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.DuplicateProduct
{
    public class ProductPhoto
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }
    }
}
