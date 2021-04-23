using AutoMapper;
using AW.UI.Web.Internal.Common;
using m = AW.UI.Web.Internal.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesReasonViewModel : IMapFrom<m.SalesReason>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.SalesReason, SalesReasonViewModel>();
        }
    }
}