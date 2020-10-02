using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.SalesPersonService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class SalesPersonViewModel : IMapFrom<CustomerService.SalesPerson>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Sales person")]
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPerson, SalesPersonViewModel>();
            profile.CreateMap<SalesPerson1, SalesPersonViewModel>();
            profile.CreateMap<SalesPersonViewModel, UpdateSalesPerson>();
            profile.CreateMap<SalesPersonDto1, SalesPersonViewModel>();
        }
    }
}