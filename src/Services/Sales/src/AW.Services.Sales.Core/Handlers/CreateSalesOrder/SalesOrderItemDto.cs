using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class SalesOrderItemDto : IMapFrom<GetSalesOrder.SalesOrderLineDto>
    {
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        public short OrderQty { get; set; }
    }
}