using AW.SharedKernel.AutoMapper;
using System.Reflection;
using AW.UI.Web.SharedKernel.Interfaces.Api;

namespace AW.UI.Web.Store.Mvc
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(IProductApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.Customer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.Customer>()
                .Include<SharedKernel.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
                .Include<SharedKernel.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
        }
    }
}