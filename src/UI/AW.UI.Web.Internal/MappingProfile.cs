using AW.UI.Web.Internal.Common;
using AW.UI.Web.Internal.ViewModels.Customer;
using m = AW.UI.Web.Internal.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            CreateMap<m.GetCustomers.Customer, CustomerViewModel>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .Include<m.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<m.GetCustomers.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<m.GetCustomer.Customer, CustomerViewModel>()
                //.ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .Include<m.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<m.GetCustomer.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<m.GetCustomer.Customer, m.UpdateCustomer.Customer>()
                .Include<m.GetCustomer.IndividualCustomer, m.UpdateCustomer.IndividualCustomer>()
                .Include<m.GetCustomer.StoreCustomer, m.UpdateCustomer.StoreCustomer>();
        }
    }
}