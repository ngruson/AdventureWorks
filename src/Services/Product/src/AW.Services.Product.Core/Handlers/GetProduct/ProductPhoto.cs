﻿using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class ProductPhoto : IMapFrom<Entities.ProductPhoto>
    {
        public byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        public string? LargePhotoFileName { get; set; }
    }
}
