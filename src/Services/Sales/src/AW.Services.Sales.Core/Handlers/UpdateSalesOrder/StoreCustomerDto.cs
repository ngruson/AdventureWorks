using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<Entities.StoreCustomer>
    {
        public string Name { get; set; }
    }    
}