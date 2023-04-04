using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using System.Reflection;

namespace AW.UI.Web.Admin.Mvc
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(ICustomerApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<SharedKernel.Customer.Handlers.GetCustomers.Customer, CustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.Customer, CustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.Customer, SharedKernel.Customer.Handlers.UpdateCustomer.Customer>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>();


            CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.Customer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.Customer>()
                .Include<SharedKernel.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
                .Include<SharedKernel.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
        }
    }
}
