namespace AW.Core.Abstractions.Api.CustomerApi.ListCustomers
{
    public class StateProvince
    {
        public string StateProvinceCode { get; set; }
        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }
        public string SalesTerritoryName { get; set; }
    }
}