using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<Entities.StoreCustomer>
    {
        public string? Name { get; set; }
    }    
}