using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public class SalesReason : IMapFrom<GetSalesOrder.SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }
    }
}