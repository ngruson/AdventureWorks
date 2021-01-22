using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.GetSalesOrder
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