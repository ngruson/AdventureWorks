using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class SalesReason : IMapFrom<Entities.SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }
    }
}
