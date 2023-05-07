using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.HumanResources.Core.Entities
{
    public class Shift : IAggregateRoot
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }

        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
