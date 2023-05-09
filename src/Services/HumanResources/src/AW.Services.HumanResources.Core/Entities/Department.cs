using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.HumanResources.Core.Entities
{
    public class Department : IAggregateRoot
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }

        public string? Name { get; set; }

        public string? GroupName { get; set; }
    }
}
