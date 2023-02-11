using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Store.ViewModels.SalesOrder
{
    public class Address
    {
        [Required]
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        [Required]
        public string? CountryRegionCode { get; set; }
    }
}