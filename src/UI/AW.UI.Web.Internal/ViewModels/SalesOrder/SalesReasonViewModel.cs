using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesOrderService;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesReasonViewModel : IMapFrom<SalesReason>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReason, SalesReasonViewModel>();
            profile.CreateMap<SalesReason1, SalesReasonViewModel>();
        }
    }
}