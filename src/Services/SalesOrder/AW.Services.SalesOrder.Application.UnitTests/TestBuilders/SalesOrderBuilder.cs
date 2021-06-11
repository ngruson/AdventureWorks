namespace AW.Services.SalesOrder.Application.UnitTests.TestBuilders
{
    public class SalesOrderBuilder
    {
        private Domain.SalesOrder salesOrder = new Domain.SalesOrder();

        public SalesOrderBuilder SalesOrderNumber(string salesOrderNumber)
        {
            salesOrder.SalesOrderNumber = salesOrderNumber;
            return this;
        }

        public Domain.SalesOrder Build()
        {
            return salesOrder;
        }

        public SalesOrderBuilder WithTestValues()
        {
            salesOrder = new Domain.SalesOrder
            {
                SalesOrderNumber = "SO43659"
            };

            return this;
        }
    }
}