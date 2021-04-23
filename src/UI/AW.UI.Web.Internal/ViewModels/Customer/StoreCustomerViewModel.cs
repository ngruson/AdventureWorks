using AutoMapper;
using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Internal.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StoreCustomerViewModel : CustomerViewModel, IMapFrom<ApiClients.CustomerApi.Models.GetCustomers.StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }

        [Display(Name = "Sales person")]
        public string SalesPerson { get; set; }
        public List<CustomerContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApiClients.CustomerApi.Models.GetCustomers.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<StoreCustomerViewModel, ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>()
                .ForMember(m => m.Name, opt => opt.Ignore());
        }
    }
}