using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder
{
    public class SalesReason : IMapFrom<GetSalesOrder.SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }
    }
}