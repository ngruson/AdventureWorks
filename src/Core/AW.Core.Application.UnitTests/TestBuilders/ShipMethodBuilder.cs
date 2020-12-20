using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class ShipMethodBuilder
    {
        private Domain.Purchasing.ShipMethod shipMethod = new Domain.Purchasing.ShipMethod();

        public ShipMethodBuilder Id(int id)
        {
            shipMethod.Id = id;
            return this;
        }

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

        public Domain.Purchasing.ShipMethod Build()
        {
            return shipMethod;
        }

        public ShipMethodBuilder WithTestValues()
        {
            shipMethod = new Domain.Purchasing.ShipMethod
            {
                Id = new Random().Next(),
                Name = "XRQ - TRUCK GROUND",
                ShipBase = 3.95M,
                ShipRate = 0.99M
            };

            return this;
        }
    }
}