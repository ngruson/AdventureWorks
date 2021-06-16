﻿using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.UnitTests.TestBuilders
{
    public class SpecialOfferProductBuilder
    {
        private SpecialOfferProduct specialOfferProduct = new SpecialOfferProduct();

        public SpecialOfferProductBuilder SpecialOffer(SpecialOffer specialOffer)
        {
            specialOfferProduct.SpecialOffer = specialOffer;
            return this;
        }

        public SpecialOfferProductBuilder ProductNumber(string productNumber)
        {
            specialOfferProduct.ProductNumber = productNumber;
            return this;
        }

        public SpecialOfferProduct Build()
        {
            return specialOfferProduct;
        }

        public SpecialOfferProductBuilder WithTestValues()
        {
            specialOfferProduct = new SpecialOfferProduct
            {
                SpecialOffer = new SpecialOfferBuilder()
                    .WithTestValues()
                    .Build(),
                ProductNumber = "FR-R92B-58"
            };

            return this;
        }
    }
}