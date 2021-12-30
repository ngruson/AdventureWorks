using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;
using api = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class Address : IMapFrom<api.Address>
    {
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        [Required]
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, api.Address>()
                .ReverseMap();
        }
    }
}