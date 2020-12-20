using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class SpecialOfferProductBuilder
    {
        private Domain.Sales.SpecialOfferProduct specialOfferProduct = new Domain.Sales.SpecialOfferProduct();


        public SpecialOfferProductBuilder ProductID(int productID)
        {
            specialOfferProduct.ProductID = productID;
            return this;
        }

        public SpecialOfferProductBuilder Product(Domain.Production.Product product)
        {
            specialOfferProduct.Product = product;
            return this;
        }

        public SpecialOfferProductBuilder SpecialOfferID(int specialOfferID)
        {
            specialOfferProduct.SpecialOfferID = specialOfferID;
            return this;
        }

        public SpecialOfferProductBuilder SpecialOffer(Domain.Sales.SpecialOffer specialOffer)
        {
            specialOfferProduct.SpecialOffer = specialOffer;
            return this;
        }

        public Domain.Sales.SpecialOfferProduct Build()
        {
            return specialOfferProduct;
        }

        public SpecialOfferProductBuilder WithTestValues()
        {
            specialOfferProduct = new Domain.Sales.SpecialOfferProduct
            {
                ProductID = 776,
                Product = new ProductBuilder().WithTestValues().Build(),
                SpecialOfferID = 1,
                SpecialOffer = new SpecialOfferBuilder().WithTestValues().Build(),
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            return this;
        }
    }
}