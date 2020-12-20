using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrders;

namespace AW.SalesOrderService.Messages.ListSalesOrders
{
    public class ShipMethod : IMapFrom<ShipMethodDto>
    {
        public string Name { get; set; }

        public decimal ShipBase { get; set; }

        public decimal ShipRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipMethodDto, ShipMethod>();
        }
    }
}