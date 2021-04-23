namespace AW.Services.ReferenceData.Application.UnitTests.TestBuilders
{
    public class StateProvinceBuilder
    {
        private Domain.StateProvince stateProvince = new Domain.StateProvince();

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

        public StateProvinceBuilder CountryRegion(Domain.CountryRegion countryRegion)
        {
            stateProvince.CountryRegion = countryRegion;
            return this;
        }

        public Domain.StateProvince Build()
        {
            return stateProvince;
        }

        public StateProvinceBuilder WithTestValues()
        {
            stateProvince = new Domain.StateProvince
            {
                Id = 1,
                StateProvinceCode = "AB",
                Name = "Alberta",
                IsOnlyStateProvinceFlag = false,
                CountryRegionCode = "CA",
                CountryRegion = new Domain.CountryRegion
                {
                    CountryRegionCode = "CA",
                    Name = "Canada"
                }
            };

            return this;
        }
    }
}