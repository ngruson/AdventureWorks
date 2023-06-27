using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities;

public class AddressType : IAggregateRoot
{
    public AddressType(string name)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public Guid ObjectId { get; private set; }
    
    public string Name { get; private set; }
}
