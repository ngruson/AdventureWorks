using AutoMapper;
using System.ComponentModel.DataAnnotations;
using AW.UI.Web.Internal.Common;
using m = AW.UI.Web.Internal.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class AddressViewModel : IMapFrom<m.GetCustomers.Address>
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
        [Display(Name = "State/province")]
        public string StateProvinceCode { get; set; }
        [Display(Name = "Country")]
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomers.Address, AddressViewModel>();
            profile.CreateMap<m.GetCustomer.Address, AddressViewModel>()
                .ReverseMap();

            profile.CreateMap<AddressViewModel, m.UpdateCustomer.Address>();
        }
    }
}