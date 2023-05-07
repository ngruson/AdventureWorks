namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces
{
    public class StateProvince
    {
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        public string? Name { get; set; }
    }
}