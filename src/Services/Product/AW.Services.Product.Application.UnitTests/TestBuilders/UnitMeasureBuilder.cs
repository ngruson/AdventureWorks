namespace AW.Services.Product.Application.UnitTests.TestBuilders
{
    public class UnitMeasureBuilder
    {
        private Domain.UnitMeasure unitMeasure = new Domain.UnitMeasure();

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

        public Domain.UnitMeasure Build()
        {
            return unitMeasure;
        }

        public UnitMeasureBuilder WithTestValues()
        {
            unitMeasure = new Domain.UnitMeasure
            {
                UnitMeasureCode = "CM",
                Name = "Centimeter"
            };

            return this;
        }
    }
}