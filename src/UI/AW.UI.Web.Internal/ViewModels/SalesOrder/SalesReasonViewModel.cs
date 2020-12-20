using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Infrastructure.Api.WCF.SalesOrderService;

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