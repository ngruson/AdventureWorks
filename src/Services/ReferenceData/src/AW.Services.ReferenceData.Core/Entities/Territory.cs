using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class Territory : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string CountryRegionCode { get; private set; }
        public string Group { get; private set; }
    }
}