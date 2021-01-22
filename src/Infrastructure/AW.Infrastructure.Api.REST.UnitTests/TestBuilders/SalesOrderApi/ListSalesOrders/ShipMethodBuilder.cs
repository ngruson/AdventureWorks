using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.ListSalesOrders
{
    public class ShipMethodBuilder
    {
        private ShipMethod shipMethod = new ShipMethod();

        public ShipMethodBuilder Name(string name)
        {
            shipMethod.Name = name;
            return this;
        }

        public ShipMethodBuilder ShipBase(decimal shipBase)
        {
            shipMethod.ShipBase = shipBase;
            return this;
        }

        public ShipMethodBuilder ShipRate(decimal shipRate)
        {
            shipMethod.ShipRate = shipRate;
            return this;
        }

        public ShipMethod Build()
        {
            return shipMethod;
        }

        public ShipMethodBuilder WithTestValues()
        {
            shipMethod = new ShipMethod
            {
                Name = "CARGO TRANSPORT 5",
                ShipBase = 8.99M,
                ShipRate = 1.49M
            };

            return this;
        }
    }
}