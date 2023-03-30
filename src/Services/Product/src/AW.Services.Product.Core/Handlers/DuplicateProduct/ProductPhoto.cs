using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.DuplicateProduct
{
    public class ProductPhoto : IMapFrom<CreateProduct.ProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }
    }
}
