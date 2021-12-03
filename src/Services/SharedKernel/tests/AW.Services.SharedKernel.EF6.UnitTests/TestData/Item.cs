using AW.SharedKernel.Domain;

namespace AW.Services.SharedKernel.EFCore.UnitTests.TestData
{
    public class Item : Entity
    {
        public int Id { get; set; }     

        public string Name { get; set; }
    }
}