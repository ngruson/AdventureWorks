using AutoMapper;
using AW.Common.AutoMapper;
using customerApi = AW.UI.Web.Common.ApiClients.CustomerApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StoreCustomerViewModel : CustomerViewModel, IMapFrom<customerApi.GetCustomers.StoreCustomer>
    {
        public override customerApi.GetCustomers.CustomerType CustomerType => customerApi.GetCustomers.CustomerType.Store;
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