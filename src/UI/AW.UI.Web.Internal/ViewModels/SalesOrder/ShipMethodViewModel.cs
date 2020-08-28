using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesOrderService;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class ShipMethodViewModel : IMapFrom<ShipMethod>
    {
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipMethod, ShipMethodViewModel>();
            profile.CreateMap<ShipMethod1, ShipMethodViewModel>();
        }
    }
}