using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public class StoreCustomer : Customer, IMapFrom<GetSalesOrder.StoreCustomer>
    {
        public string? Name { get; set; }
        public override string? CustomerName => Name;
    }
}