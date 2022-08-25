using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Entities
{
    public abstract class Person
    {
        public int Id { get; set; }

        public string? Title { get; private set; }

        public NameFactory? Name { get; private set; }
        public string? Suffix { get; private set; }

        public void SetName(NameFactory name)
        {
            Name = name;
        }
    }
}