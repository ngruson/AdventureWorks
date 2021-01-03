using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using GetSalesPerson = AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using System.ComponentModel.DataAnnotations;

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
            profile.CreateMap<GetSalesPerson.SalesPerson, SalesPersonViewModel>();
        }
    }
}