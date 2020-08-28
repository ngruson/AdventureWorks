using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.SalesOrder;

namespace AW.SalesOrderService.Messages
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