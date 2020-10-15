using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class AddressViewModel : IMapFrom<Address>
    {
        [Display(Name = "Address line 1")]
        [Required]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Postal code")]
        [Required]
        public string PostalCode { get; set; }
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }
        public StateProvinceViewModel StateProvince { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressViewModel>();
            profile.CreateMap<Address1, AddressViewModel>();
            profile.CreateMap<AddressViewModel, Address3>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvince.StateProvinceCode));
        }
    }
}