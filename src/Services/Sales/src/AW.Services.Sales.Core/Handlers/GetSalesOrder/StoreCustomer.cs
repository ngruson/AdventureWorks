using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class StoreCustomer : Customer, IMapFrom<Entities.StoreCustomer>
    {
        public string? Name { get; set; }
        public override string? CustomerName => Name;
    }    
}
