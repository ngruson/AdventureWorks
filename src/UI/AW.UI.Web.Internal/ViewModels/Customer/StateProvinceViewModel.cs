using AutoMapper;
using AW.Application.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StateProvinceViewModel : IMapFrom<CustomerService.StateProvince>
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
            profile.CreateMap<CustomerService.StateProvince, StateProvinceViewModel>();
            profile.CreateMap<CustomerService.StateProvince1, StateProvinceViewModel>();
            profile.CreateMap<StateProvinceService.StateProvince, StateProvinceViewModel>();
        }
    }
}