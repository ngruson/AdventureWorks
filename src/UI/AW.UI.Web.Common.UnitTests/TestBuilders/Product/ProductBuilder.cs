using Models = AW.UI.Web.Common.ApiClients.ProductApi.Models;

namespace AW.UI.Web.Common.UnitTests.TestBuilders.Product
{
    public class ProductBuilder
    {
        private Models.Product product = new();

        public ProductBuilder Name(string name)
        {
            product.Name = name;
            return this;
        }

        public ProductBuilder ProductNumber(string productNumber)
        {
            product.ProductNumber = productNumber;
            return this;
        }

        public ProductBuilder Color(string color)
        {
            product.Color = color;
            return this;
        }

        public ProductBuilder ListPrice(decimal listPrice)
        {
            product.ListPrice = listPrice;
            return this;
        }

        public ProductBuilder Size(string size)
        {
            product.Size = size;
            return this;
        }

        public ProductBuilder SizeUnitMeasureCode(string sizeUnitMeasureCode)
        {
            product.SizeUnitMeasureCode = sizeUnitMeasureCode;
            return this;
        }

        public ProductBuilder Weight(decimal weight)
        {
            product.Weight = weight;
            return this;
        }

        public ProductBuilder WeightUnitMeasureCode(string weightUnitMeasureCode)
        {
            product.WeightUnitMeasureCode = weightUnitMeasureCode;
            return this;
        }

        public ProductBuilder ProductLine(string productLine)
        {
            product.ProductLine = productLine;
            return this;
        }

        public ProductBuilder Class(string @class)
        {
            product.Class = @class;
            return this;
        }

        public ProductBuilder Style(string style)
        {
            product.Style = style;
            return this;
        }

        public ProductBuilder ProductSubcategoryName(string productSubcategoryName)
        {
            product.ProductSubcategoryName = productSubcategoryName;
            return this;
        }

        public ProductBuilder ProductCategoryName(string productCategoryName)
        {
            product.ProductCategoryName = productCategoryName;
            return this;
        }

        public ProductBuilder ThumbnailPhoto(byte[] thumbnailPhoto)
        {
            product.ThumbnailPhoto = thumbnailPhoto;
            return this;
        }

        public ProductBuilder LargePhoto(byte[] largePhoto)
        {
            product.LargePhoto = largePhoto;
            return this;
        }

        public ProductBuilder WithTestValues()
        {
            product = new Models.Product
            {
                Name = "LL Bottom Bracket",
                ProductNumber = "BB-7421",
                Color = null,
                ListPrice = 53.99M,
                Size = null,
                SizeUnitMeasureCode = null,
                Weight = 223,
                WeightUnitMeasureCode = "G",
                ProductLine = null,
                Class = "L",
                Style = null,
                ProductCategoryName = "Components",
                ProductSubcategoryName = "Bottom Brackets",
                ThumbnailPhoto = null,
                LargePhoto = null
            };

            return this;
        }

        public Models.Product Build()
        {
            return product;
        }
    }
}