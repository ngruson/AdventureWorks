using AW.SharedKernel.AutoMapper;
using System.Reflection;
using AW.UI.Web.Infrastructure.Api.Interfaces;

namespace AW.UI.Web.Store.Mvc
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(IProductApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.Customer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.Customer>()
                .Include<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
                .Include<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
        }
    }
}
