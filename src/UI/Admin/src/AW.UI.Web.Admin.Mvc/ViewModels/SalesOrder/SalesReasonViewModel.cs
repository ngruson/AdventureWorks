using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class SalesReasonViewModel : IMapFrom<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesReason, SalesReasonViewModel>();
            profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesReason, SalesReasonViewModel>();
        }
    }
}
