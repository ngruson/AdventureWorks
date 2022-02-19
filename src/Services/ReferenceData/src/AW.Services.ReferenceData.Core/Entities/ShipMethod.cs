using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class ShipMethod : IAggregateRoot
    {
        private int Id { get; set; }
        public string Name { get; private set; }
        public decimal ShipBase { get; private set; }
        public decimal ShipRate { get; private set; }
    }
}