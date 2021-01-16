using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AW.Core.Domain.Sales;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerViewModel : IMapFrom<GetCustomer.Customer>
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
            profile.CreateMap<GetCustomer.Customer, CustomerViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => MapName(src) ))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => MapCustomerType(src)));

            profile.CreateMap<ListCustomers.Customer, CustomerViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => MapName(src) ))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => MapCustomerType(src)));

            profile.CreateMap<CustomerViewModel, UpdateCustomer.UpdateCustomerRequest>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src));
            profile.CreateMap<CustomerViewModel, UpdateCustomer.Customer>();
        }

        private string MapName(GetCustomer.Customer src)
        {
            if (src.Store != null)
                return src.Store.Name;

            if (src.Person != null)
                return src.Person.FullName;

            return null;
        }

        private string MapName(ListCustomers.Customer src)
        {
            if (src.Store != null)
                return src.Store.Name;

            if (src.Person != null)
                return src.Person.FullName;

            return null;
        }

        private string MapCustomerType(GetCustomer.Customer customer)
        {
            if (customer.Store != null)
                return "Store";

            if (customer.Person != null)
                return "Individual";

            return null;
        }

        private string MapCustomerType(ListCustomers.Customer customer)
        {
            if (customer.Store != null)
                return "Store";

            if (customer.Person != null)
                return "Individual";

            return null;
        }
    }
}