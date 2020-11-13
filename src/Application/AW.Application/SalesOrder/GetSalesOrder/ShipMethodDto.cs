using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Purchasing;

namespace AW.Application.SalesOrder.GetSalesOrder
{
    public class ShipMethodDto : IMapFrom<ShipMethod>
    {
        public string Name { get; set; }

        public decimal ShipBase { get; set; }

        public decimal ShipRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipMethod, ShipMethodDto>();
        }
    }
}