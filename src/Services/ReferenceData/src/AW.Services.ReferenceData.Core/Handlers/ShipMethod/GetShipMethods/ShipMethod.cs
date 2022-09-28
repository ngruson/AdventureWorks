using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods
{
    public class ShipMethod : IMapFrom<Entities.ShipMethod>
    {
        public string? Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
    }
}