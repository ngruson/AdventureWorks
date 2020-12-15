using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerViewModel : IMapFrom<CustomerService.Customer>
    {
        [Display(Name="Account number")]
        public string AccountNumber { get; set; }
        public string Name { get; set; }

        [Display(Name = "Sales territory")]
        public string SalesTerritoryName { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerPersonViewModel Person { get; set; }
        public CustomerStoreViewModel Store { get; set; }
        public List<SalesOrderViewModel> SalesOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerService.Customer, CustomerViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Store.Name ?? src.Person.FullName))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => MapCustomerType(src)));

            profile.CreateMap<Customer1, CustomerViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Store.Name ?? src.Person.FullName))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => MapCustomerType(src)));

            profile.CreateMap<CustomerViewModel, UpdateCustomer>();
        }

        private string MapCustomerType(CustomerService.Customer customer)
        {
            if (customer.Store != null)
                return "Store";

            if (customer.Person != null)
                return "Individual";

            return null;
        }

        private string MapCustomerType(Customer1 customer)
        {
            if (customer.Store != null)
                return "Store";

            if (customer.Person != null)
                return "Individual";

            return null;
        }
    }
}