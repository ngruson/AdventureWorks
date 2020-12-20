using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using System.ComponentModel.DataAnnotations;
using AW.Infrastructure.Api.WCF.SalesPersonService;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class SalesPersonViewModel : IMapFrom<GetCustomer.SalesPerson>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Sales person")]
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.SalesPerson, SalesPersonViewModel>();
            profile.CreateMap<ListCustomers.SalesPerson, SalesPersonViewModel>();
            profile.CreateMap<SalesPersonViewModel, UpdateCustomer.SalesPerson>();
            profile.CreateMap<SalesPersonDto1, SalesPersonViewModel>();
        }
    }
}