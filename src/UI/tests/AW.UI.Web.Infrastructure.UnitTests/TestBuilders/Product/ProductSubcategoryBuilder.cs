using AW.UI.Web.Infrastructure.ApiClients.ProductApi.Models;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.Product
{
    public class ProductSubcategoryBuilder
    {
        private ProductSubcategory productSubcategory = new();

        public ProductSubcategoryBuilder Name(string name)
        {
            productSubcategory.Name = name;
            return this;
        }

        public ProductSubcategoryBuilder WithTestValues()
        {
            productSubcategory = new ProductSubcategory
            {
                Name = "Bottom Brackets",
                ProductCount = 3
            };

            return this;
        }

        public ProductSubcategory Build()
        {
            return productSubcategory;
        }
    }
}