using AutoMapper;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StoreCustomerViewModel : CustomerViewModel, IMapFrom<customerApi.GetCustomers.StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }

        [Display(Name = "Sales person")]
        public string SalesPerson { get; set; }
        public List<CustomerContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<customerApi.GetCustomers.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<customerApi.GetCustomer.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<StoreCustomerViewModel, customerApi.UpdateCustomer.StoreCustomer>()
                .ForMember(m => m.Name, opt => opt.Ignore());
        }
    }
}