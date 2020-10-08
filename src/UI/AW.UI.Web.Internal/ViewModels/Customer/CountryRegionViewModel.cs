using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CountryRegionViewModel : IMapFrom<CountryRegion>
    {
        [Display(Name = "Country")]
        [Required]
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CountryRegion, CountryRegionViewModel>();
            profile.CreateMap<CountryRegion1, CountryRegionViewModel>();
        }
    }
}