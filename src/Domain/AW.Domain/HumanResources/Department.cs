using AW.Domain.Person;

namespace AW.Domain.HumanResources
{
    public class Department : BusinessEntity
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}