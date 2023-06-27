using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities;

public class ShipMethod : IAggregateRoot
{
    public ShipMethod(string name, decimal shipBase, decimal shipRate)
    {
        Name = name;
        ShipBase = shipBase;
        ShipRate = shipRate;
    }

    public int Id { get; private set; }
    public Guid ObjectId { get; private set; }
    public string Name { get; private set; }
    public decimal ShipBase { get; private set; }
    public decimal ShipRate { get; private set; }
}
