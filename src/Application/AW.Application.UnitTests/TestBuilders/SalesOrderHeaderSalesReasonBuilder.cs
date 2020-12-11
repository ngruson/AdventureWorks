namespace AW.Application.UnitTests.TestBuilders
{
    public class SalesOrderHeaderSalesReasonBuilder
    {
        private Domain.Sales.SalesOrderHeaderSalesReason salesOrderHeaderSalesReason = new Domain.Sales.SalesOrderHeaderSalesReason();

        public SalesOrderHeaderSalesReasonBuilder SalesOrderID(int salesOrderID)
        {
            salesOrderHeaderSalesReason.SalesOrderID = salesOrderID;
            return this;
        }

        public SalesOrderHeaderSalesReasonBuilder SalesReasonID(int salesReasonID)
        {
            salesOrderHeaderSalesReason.SalesReasonID = salesReasonID;
            return this;
        }

        public SalesOrderHeaderSalesReasonBuilder SalesReason(Domain.Sales.SalesReason salesReason)
        {
            salesOrderHeaderSalesReason.SalesReason = salesReason;
            return this;
        }

        public Domain.Sales.SalesOrderHeaderSalesReason Build()
        {
            return salesOrderHeaderSalesReason;
        }

        public SalesOrderHeaderSalesReasonBuilder WithTestValues()
        {
            salesOrderHeaderSalesReason = new Domain.Sales.SalesOrderHeaderSalesReason
            {
                SalesOrderID = 43659,
                SalesReasonID = 5,
                SalesReason = new SalesReasonBuilder()
                    .Name("Price")
                    .ReasonType("Other")
                    .Build()
            };

            return this;
        }
    }
}