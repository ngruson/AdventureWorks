namespace AW.Domain.HumanResources
{
    public class Department
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}