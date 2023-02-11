using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }        
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
    }
}