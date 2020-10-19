using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesTerritoryService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class CountryRegionViewModel : IMapFrom<CountryRegionDto>
    {
        [Display(Name = "Country")]
        [Required]
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CountryRegionDto, CountryRegionViewModel>();
        }
    }
}