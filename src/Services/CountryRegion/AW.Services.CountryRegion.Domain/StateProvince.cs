namespace AW.Services.CountryRegion.Domain
{
    public class StateProvince
    {
        public int Id { get; set; }
        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }

        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }
    }
}