namespace AW.Services.Product.Core.UnitTests.TestBuilders
{
    public class ProductCategoryBuilder
    {
        private Entities.ProductCategory category = new();

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

        public Entities.ProductCategory Build()
        {
            return category;
        }

        public ProductCategoryBuilder WithTestValues()
        {
            category = new Entities.ProductCategory
            {
                Id = 2,
                Name = "Components"
            };

            return this;
        }
    }
}