using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;
using s = AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class AddressViewModel : IMapFrom<s.Address>
    {
        [Display(Name = "Address line 1" )]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        [Display(Name = "State/province")]
        public string StateProvinceCode { get; set; }
        [Display(Name = "Country")]
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<s.Address, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"));
        }
    }
}