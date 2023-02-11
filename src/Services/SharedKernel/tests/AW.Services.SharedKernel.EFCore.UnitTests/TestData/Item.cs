using AW.Services.SharedKernel.Domain;

namespace AW.Services.SharedKernel.EFCore.UnitTests.TestData
{
    public class Item : Entity
    {
        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }     

        public string Name { get; set; }
    }
}