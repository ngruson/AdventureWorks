using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Entities
{
    public abstract class Customer : IAggregateRoot
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; }
        public abstract CustomerType CustomerType { get; }
        public abstract string FullName { get; }
        public string Territory { get; set; }
    }
}