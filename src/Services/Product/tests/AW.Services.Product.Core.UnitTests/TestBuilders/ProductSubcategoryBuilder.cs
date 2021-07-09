using System;

namespace AW.Services.Product.Core.UnitTests.TestBuilders
{
    public class ProductSubcategoryBuilder
    {
        private Entities.ProductSubcategory subCategory = new Entities.ProductSubcategory();

        public ProductSubcategoryBuilder Id(int id)
        {
            subCategory.Id = id;
            return this;
        }

        public ProductSubcategoryBuilder Name(string name)
        {
            subCategory.Name = name;
            return this;
        }

        public ProductSubcategoryBuilder ProductCategoryId(int productCategoryId)
        {
            subCategory.ProductCategoryId = productCategoryId;
            return this;
        }

        public ProductSubcategoryBuilder ProductCategory(Entities.ProductCategory productCategory)
        {
            subCategory.ProductCategory = productCategory;
            return this;
        }

        public Entities.ProductSubcategory Build()
        {
            return subCategory;
        }

        public ProductSubcategoryBuilder WithTestValues()
        {
            subCategory = new Entities.ProductSubcategory
            {
                Id = 14,
                Name = "Road Frames",
                ProductCategoryId = 2,
                ProductCategory = new ProductCategoryBuilder()
                    .WithTestValues()
                    .Id(2)
                    .Name("Components")
                    .Build()
            };

            return this;
        }
    }
}