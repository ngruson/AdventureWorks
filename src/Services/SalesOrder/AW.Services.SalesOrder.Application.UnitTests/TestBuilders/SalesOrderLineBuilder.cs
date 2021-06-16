using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.UnitTests.TestBuilders
{
    public class SalesOrderLineBuilder
    {
        private SalesOrderLine salesOrderLine = new SalesOrderLine();

        public SalesOrderLineBuilder CarrierTrackingNumber(string carrierTrackingNumber)
        {
            salesOrderLine.CarrierTrackingNumber = carrierTrackingNumber;
            return this;
        }

        public SalesOrderLineBuilder OrderQty(short orderQty)
        {
            salesOrderLine.OrderQty = orderQty;
            return this;
        }

        public SalesOrderLineBuilder ProductNumber(string productNumber)
        {
            salesOrderLine.ProductNumber = productNumber;
            return this;
        }

        public SalesOrderLineBuilder ProductName(string productName)
        {
            salesOrderLine.ProductName = productName;
            return this;
        }

        public SalesOrderLineBuilder UnitPrice(decimal unitPrice)
        {
            salesOrderLine.UnitPrice = unitPrice;
            return this;
        }

        public SalesOrderLineBuilder UnitPriceDiscount(decimal unitPriceDiscount)
        {
            salesOrderLine.UnitPriceDiscount = unitPriceDiscount;
            return this;
        }

        public SalesOrderLineBuilder SpecialOfferProduct(SpecialOfferProduct specialOfferProduct)
        {
            salesOrderLine.SpecialOfferProduct = specialOfferProduct;
            return this;
        }

        public SalesOrderLine Build()
        {
            return salesOrderLine;
        }

        public SalesOrderLineBuilder WithTestValues()
        {
            salesOrderLine = new SalesOrderLine
            {
                CarrierTrackingNumber = "4911-403C-98",
                OrderQty = 1,
                ProductNumber = "BK-M82B-42",
                ProductName = "Mountain-100 Black, 42",
                SpecialOfferProduct = new SpecialOfferProductBuilder()
                    .WithTestValues()
                    .Build()
            };

            return this;
        }
    }
}