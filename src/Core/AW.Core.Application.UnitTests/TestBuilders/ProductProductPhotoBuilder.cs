using AW.Core.Domain.Production;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class ProductProductPhotoBuilder
    {
        private ProductProductPhoto productProductPhoto = new ProductProductPhoto();

        public ProductProductPhotoBuilder Primary(bool primary)
        {
            productProductPhoto.Primary = primary;
            return this;
        }

        public ProductProductPhotoBuilder ProductPhoto(ProductPhoto productPhoto)
        {
            productProductPhoto.ProductPhoto = productPhoto;
            return this;
        }

        public ProductProductPhoto Build()
        {
            return productProductPhoto;
        }

        public ProductProductPhotoBuilder WithTestValues()
        {
            productProductPhoto = new ProductProductPhoto
            {
                Primary = true,
                ProductPhoto = new ProductPhotoBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}