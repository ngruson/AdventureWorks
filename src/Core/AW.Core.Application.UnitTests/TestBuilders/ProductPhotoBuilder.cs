using AW.Core.Domain.Production;
using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class ProductPhotoBuilder
    {
        private ProductPhoto productPhoto = new ProductPhoto();

        public ProductPhotoBuilder Id(int id)
        {
            productPhoto.Id = id;
            return this;
        }

        public ProductPhotoBuilder ThumbNailPhoto(byte[] thumbNailPhoto)
        {
            productPhoto.ThumbNailPhoto = thumbNailPhoto;
            return this;
        }

        public ProductPhotoBuilder ThumbNailPhotoFileName(string thumbNailPhotoFileName)
        {
            productPhoto.ThumbnailPhotoFileName = thumbNailPhotoFileName;
            return this;
        }

        public ProductPhotoBuilder LargePhoto(byte[] largePhoto)
        {
            productPhoto.LargePhoto = largePhoto;
            return this;
        }

        public ProductPhotoBuilder LargePhotoFileName(string largePhotoFileName)
        {
            productPhoto.LargePhotoFileName = largePhotoFileName;
            return this;
        }

        public ProductPhoto Build()
        {
            return productPhoto;
        }

        public ProductPhotoBuilder WithTestValues()
        {
            productPhoto = new ProductPhoto
            {
                ThumbNailPhoto = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw=="),
                ThumbnailPhotoFileName = "no_image_available_small.gif",
                LargePhoto = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw=="),
                LargePhotoFileName = "no_image_available_large.gif"
            };

            return this;
        }
    }
}