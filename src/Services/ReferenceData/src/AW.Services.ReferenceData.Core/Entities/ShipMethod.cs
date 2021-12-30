using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class ShipMethod : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
    }
}