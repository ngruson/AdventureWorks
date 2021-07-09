namespace AW.Services.ReferenceData.Core.UnitTests.TestBuilders
{
    public class StateProvinceBuilder
    {
        private Entities.StateProvince stateProvince = new();

        public StateProvinceBuilder StateProvinceCode(string stateProvinceCode)
        {
            stateProvince.StateProvinceCode = stateProvinceCode;
            return this;
        }

        public StateProvinceBuilder Name(string name)
        {
            stateProvince.Name = name;
            return this;
        }

        public StateProvinceBuilder IsOnlyStateProvinceFlag(bool isOnlyStateProvinceFlag)
        {
            stateProvince.IsOnlyStateProvinceFlag = isOnlyStateProvinceFlag;
            return this;
        }

        public StateProvinceBuilder CountryRegionCode(string countryRegionCode)
        {
            stateProvince.CountryRegionCode = countryRegionCode;
            return this;
        }

        public StateProvinceBuilder CountryRegion(Entities.CountryRegion countryRegion)
        {
            stateProvince.CountryRegion = countryRegion;
            return this;
        }

        public Entities.StateProvince Build()
        {
            return stateProvince;
        }

        public StateProvinceBuilder WithTestValues()
        {
            stateProvince = new Entities.StateProvince
            {
                Id = 1,
                StateProvinceCode = "AB",
                Name = "Alberta",
                IsOnlyStateProvinceFlag = false,
                CountryRegionCode = "CA",
                CountryRegion = new Entities.CountryRegion
                {
                    CountryRegionCode = "CA",
                    Name = "Canada"
                }
            };

            return this;
        }
    }
}