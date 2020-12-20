using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Purchasing;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
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