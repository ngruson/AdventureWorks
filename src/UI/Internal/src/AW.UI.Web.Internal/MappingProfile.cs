using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using System.Reflection;

namespace AW.UI.Web.Internal
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(ICustomerApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<SharedKernel.Customer.Handlers.GetCustomers.Customer, CustomerViewModel>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .Include<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.Customer, CustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer, StoreCustomerViewModel>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.Customer, SharedKernel.Customer.Handlers.UpdateCustomer.Customer>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
                .Include<SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>();

            CreateMap<SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>();
        }
    }
}