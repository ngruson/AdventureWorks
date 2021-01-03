using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using ListStateProvinces = AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CountryRegionViewModel : IMapFrom<GetCustomer.CountryRegion>
    {
        [Display(Name = "Country")]
        [Required]
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.CountryRegion, CountryRegionViewModel>();
            profile.CreateMap<ListCustomers.CountryRegion, CountryRegionViewModel>();
            profile.CreateMap<ListStateProvinces.StateProvince, CountryRegionViewModel>();

            profile.CreateMap<CountryRegionViewModel, GetCustomer.CountryRegion>();
            profile.CreateMap<CountryRegionViewModel, UpdateCustomer.CountryRegion>();
        }
    }
}