using System;

namespace AW.Services.Product.Core.UnitTests.TestBuilders
{
    public class ProductModelBuilder
    {
        private Entities.ProductModel productModel = new Entities.ProductModel();

        public ProductModelBuilder Id(int id)
        {
            productModel.Id = id;
            return this;
        }

        public ProductModelBuilder Name(string name)
        {
            productModel.Name = name;
            return this;
        }

        public ProductModelBuilder rowguid(Guid rowguid)
        {
            productModel.rowguid = rowguid;
            return this;
        }

        public ProductModelBuilder ModifiedDate(DateTime modifiedDate)
        {
            productModel.ModifiedDate = modifiedDate;
            return this;
        }

        public Entities.ProductModel Build()
        {
            return productModel;
        }

        public ProductModelBuilder WithTestValues()
        {
            productModel = new Entities.ProductModel
            {
                Name = "HL Road Frame",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            return this;
        }
    }
}