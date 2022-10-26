using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class StoreCustomerViewModel : CustomerViewModel, IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }

        [Display(Name = "Sales person")]
        public string SalesPerson { get; set; }
        public List<CustomerContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.StoreCustomer, StoreCustomerViewModel>();
            profile.CreateMap<StoreCustomerViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>()
                .ForMember(m => m.Name, opt => opt.Ignore());
        }
    }
}