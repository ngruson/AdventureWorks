using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetSalesOrders
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

        public SalesOrderLineBuilder ProductNumber(string productNumber)
        {
            orderLine.ProductNumber = productNumber;
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

        public SalesOrderLineBuilder WithTestValues()
        {
            orderLine = new SalesOrderLine
            {
                CarrierTrackingNumber = "4911-403C-98",
                OrderQty = 1,
                ProductNumber = "BK-M82B-42",
                ProductName = "Mountain-100 Black, 42",
                SpecialOfferDescription = "No Discount",
                UnitPrice = 2024.994M,
                UnitPriceDiscount = 0
            };

            return this;
        }

        public SalesOrderLine Build()
        {
            return orderLine;
        }
    }
}