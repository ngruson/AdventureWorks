namespace AW.Services.Product.Core.UnitTests.TestBuilders
{
    public class UnitMeasureBuilder
    {
        private Entities.UnitMeasure unitMeasure = new Entities.UnitMeasure();

        public UnitMeasureBuilder UnitMeasureCode(string unitMeasureCode)
        {
            unitMeasure.UnitMeasureCode = unitMeasureCode;
            return this;
        }

        public UnitMeasureBuilder Name(string name)
        {
            unitMeasure.Name = name;
            return this;
        }

        public Entities.UnitMeasure Build()
        {
            return unitMeasure;
        }

        public UnitMeasureBuilder WithTestValues()
        {
            unitMeasure = new Entities.UnitMeasure
            {
                UnitMeasureCode = "CM",
                Name = "Centimeter"
            };

            return this;
        }
    }
}