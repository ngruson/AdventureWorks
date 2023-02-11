using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class Address : IMapFrom<SharedKernel.Basket.Handlers.Checkout.Address>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, SharedKernel.Basket.Handlers.Checkout.Address>()
                .ReverseMap();

            profile.CreateMap<SharedKernel.Customer.Handlers.GetPreferredAddress.Address, Address>();
        }
    }
}