using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile()
        {
            CreateMap<SalesOrder.Handlers.GetSalesOrder.Customer, SalesOrder.Handlers.UpdateSalesOrder.Customer>()
                .Include<SalesOrder.Handlers.GetSalesOrder.IndividualCustomer, SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>()
                .Include<SalesOrder.Handlers.GetSalesOrder.StoreCustomer, SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer>();
        }
    }
}