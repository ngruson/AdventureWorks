using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class AddressType : IAggregateRoot
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}