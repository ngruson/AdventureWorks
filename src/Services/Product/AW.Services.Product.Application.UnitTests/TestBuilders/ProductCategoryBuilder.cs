namespace AW.Services.Product.Application.UnitTests.TestBuilders
{
    public class ProductCategoryBuilder
    {
        private Domain.ProductCategory category = new Domain.ProductCategory();

        public ProductCategoryBuilder Id(int id)
        {
            category.Id = id;
            return this;
        }

        public ProductCategoryBuilder Name(string name)
        {
            category.Name = name;
            return this;
        }

        public Domain.ProductCategory Build()
        {
            return category;
        }

        public ProductCategoryBuilder WithTestValues()
        {
            category = new Domain.ProductCategory
            {
                Id = 2,
                Name = "Components"
            };

            return this;
        }
    }
}