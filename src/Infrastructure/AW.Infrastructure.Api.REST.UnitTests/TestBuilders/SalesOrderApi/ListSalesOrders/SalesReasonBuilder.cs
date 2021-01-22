using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.ListSalesOrders
{
    public class SalesReasonBuilder
    {
        private SalesReason salesReason = new SalesReason();

        public SalesReason Build()
        {
            return salesReason;
        }

        public SalesReasonBuilder WithTestValues()
        {
            salesReason = new SalesReason
            {
                Name = "Price",
                ReasonType = "Other"
            };

            return this;
        }
    }
}