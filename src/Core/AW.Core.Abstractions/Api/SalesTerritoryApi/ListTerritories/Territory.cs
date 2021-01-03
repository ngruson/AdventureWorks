namespace AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories
{
    public class Territory
    {
        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }
        public string Group { get; set; }
    }
}