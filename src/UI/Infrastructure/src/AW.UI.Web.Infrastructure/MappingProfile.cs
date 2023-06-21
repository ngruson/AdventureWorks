using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<Api.SalesOrder.Handlers.GetSalesOrder.Customer, Api.SalesOrder.Handlers.UpdateSalesOrder.Customer>()
            .Include<Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, Api.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
            .Include<Api.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, Api.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
    }
}
