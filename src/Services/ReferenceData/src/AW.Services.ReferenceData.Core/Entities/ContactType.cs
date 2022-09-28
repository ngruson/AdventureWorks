using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class ContactType : IAggregateRoot
    {
        public ContactType(string name)
        {
            Name = name;
        }
        public int Id { get; set; }

        public string Name { get; private set; }
    }
}