using AutoMapper;
using AW.Core.Application.AutoMapper;
using System.ComponentModel.DataAnnotations;
using ListTerritories = AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class CountryRegionViewModel : IMapFrom<ListTerritories.CountryRegion>
    {
        [Display(Name = "Country")]
        [Required]
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListTerritories.CountryRegion, CountryRegionViewModel>();
        }
    }
}