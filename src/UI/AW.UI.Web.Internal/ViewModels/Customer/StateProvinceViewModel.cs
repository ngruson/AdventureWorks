using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using ListStateProvinces = AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StateProvinceViewModel : IMapFrom<GetCustomer.StateProvince>
    {
        [Display(Name = "State/province code")]
        [Required]
        public string StateProvinceCode { get; set; }

        [Display(Name = "Country")]
        [Required]
        public CountryRegionViewModel CountryRegion { get; set; }

        [Display(Name = "State/province")]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.StateProvince, StateProvinceViewModel>()
                .ReverseMap();
            profile.CreateMap<ListCustomers.StateProvince, StateProvinceViewModel>();
            profile.CreateMap<ListStateProvinces.StateProvince, StateProvinceViewModel>()
                .ForMember(m => m.CountryRegion, opt => opt.MapFrom(src => src));

            profile.CreateMap<StateProvinceViewModel, UpdateCustomer.StateProvince>();
        }
    }
}