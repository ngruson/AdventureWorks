using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesReasonViewModel : IMapFrom<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesReason>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesReason, SalesReasonViewModel>();
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.SalesReason, SalesReasonViewModel>();
        }
    }
}