using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.ListSalesOrders
{
    public class SalesOrderLineBuilder
    {
        private SalesOrderLine orderLine = new SalesOrderLine();

        public SalesOrderLineBuilder CarrierTrackingNumber(string carrierTrackingNumber)
        {
            orderLine.CarrierTrackingNumber = carrierTrackingNumber;
            return this;
        }

        public SalesOrderLineBuilder OrderQty(short orderQty)
        {
            orderLine.OrderQty = orderQty;
            return this;
        }

        public SalesOrderLineBuilder ProductName(string productName)
        {
            orderLine.ProductName = productName;
            return this;
        }

        public SalesOrderLineBuilder SpecialOfferDescription(string specialOfferDescription)
        {
            orderLine.SpecialOfferDescription = specialOfferDescription;
            return this;
        }

        public SalesOrderLineBuilder UnitPrice(decimal unitPrice)
        {
            orderLine.UnitPrice = unitPrice;
            return this;
        }

        public SalesOrderLineBuilder UnitPriceDiscount(decimal unitPriceDiscount)
        {
            orderLine.UnitPriceDiscount = unitPriceDiscount;
            return this;
        }

        public SalesOrderLineBuilder LineTotal(decimal lineTotal)
        {
            orderLine.LineTotal = lineTotal;
            return this;
        }

        public SalesOrderLine Build()
        {
            return orderLine;
        }

        public SalesOrderLineBuilder WithTestValues()
        {
            orderLine = new SalesOrderLine
            {
                CarrierTrackingNumber = "4911-403C-98",
                OrderQty = 1,
                ProductName = "Mountain-100 Black, 42",
                SpecialOfferDescription = "No Discount",
                UnitPrice = 2024.994M,
                UnitPriceDiscount = 0.00M,
                LineTotal = 2024.994000M
            };

            return this;
        }
    }
}