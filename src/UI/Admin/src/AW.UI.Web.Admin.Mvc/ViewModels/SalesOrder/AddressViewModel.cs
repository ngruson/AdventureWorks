using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class AddressViewModel : IMapFrom<SharedKernel.SalesOrder.Handlers.GetSalesOrders.Address>
    {
        [Display(Name = "Address line 1")]
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
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrders.Address, AddressViewModel>();
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.Address, AddressViewModel>();
            profile.CreateMap<AddressViewModel, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.Address>();
        }
    }
}