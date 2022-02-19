using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class AddressType : IAggregateRoot
    {
        private int Id { get; set; }
        
        public string Name { get; private set; }
    }
}