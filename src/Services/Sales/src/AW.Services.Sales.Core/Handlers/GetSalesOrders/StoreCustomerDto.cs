using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<Entities.StoreCustomer>
    {
        public string? Name { get; set; }
    }    
}