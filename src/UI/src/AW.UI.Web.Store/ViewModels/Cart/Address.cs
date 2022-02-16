using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;
using basketApi = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class Address : IMapFrom<basketApi.Address>
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
            profile.CreateMap<Address, basketApi.Address>()
                .ReverseMap();

            profile.CreateMap<customerApi.GetPreferredAddress.Address, Address>();
        }
    }
}