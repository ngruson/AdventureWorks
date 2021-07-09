using AW.UI.Web.Common.ApiClients.ProductApi.Models;
using System.Collections.Generic;

namespace AW.UI.Web.Common.UnitTests.TestBuilders.Product
{
    public class ProductCategoryBuilder
    {
        private ProductCategory productCategory = new();

        public ProductCategoryBuilder Name(string name)
        {
            productCategory.Name = name;
            return this;
        }

        public ProductCategoryBuilder WithTestValues()
        {
            productCategory = new ProductCategory
            {
                Name = "Components",
                Subcategories = new List<ProductSubcategory>
                {
                    new ProductSubcategoryBuilder().WithTestValues().Build()
                }
            };

            return this;
        }

        public ProductCategory Build()
        {
            return productCategory;
        }
    }
}