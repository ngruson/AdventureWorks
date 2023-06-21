using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using System.Reflection;

namespace AW.UI.Web.Admin.Mvc;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile() : base()
    {
        ApplyMappingsFromAssembly(typeof(ICustomerApiClient).Assembly);
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.Customer, CustomerViewModel>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomer, StoreCustomerViewModel>();

        CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.Customer, CustomerViewModel>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer, StoreCustomerViewModel>();

        CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.Customer, Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer, Infrastructure.Api.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
            .Include<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer, Infrastructure.Api.Customer.Handlers.UpdateCustomer.StoreCustomer>();

        CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer, Infrastructure.Api.Customer.Handlers.UpdateCustomer.IndividualCustomer>();


        CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.Customer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.Customer>()
            .Include<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
            .Include<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
    }
}
