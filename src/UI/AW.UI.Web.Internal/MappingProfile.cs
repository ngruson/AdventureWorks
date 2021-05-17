using AutoMapper;
using AW.Common.AutoMapper;
using AW.UI.Web.Common.ApiClients.CustomerApi;
using AW.UI.Web.Internal.ViewModels.Customer;
using System;
using System.Linq;
using System.Reflection;
using m = AW.UI.Web.Common.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(ICustomerApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<m.GetCustomers.Customer, CustomerViewModel>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .Include<m.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<m.GetCustomers.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<m.GetCustomer.Customer, CustomerViewModel>()
                .Include<m.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<m.GetCustomer.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<m.GetCustomer.Customer, m.UpdateCustomer.Customer>()
                .Include<m.GetCustomer.IndividualCustomer, m.UpdateCustomer.IndividualCustomer>()
                .Include<m.GetCustomer.StoreCustomer, m.UpdateCustomer.StoreCustomer>();
        }
    }
}