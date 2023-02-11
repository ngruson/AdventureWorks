using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Idempotency
{
    public class ClientRequest : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime Time { get; set; }
    }
}