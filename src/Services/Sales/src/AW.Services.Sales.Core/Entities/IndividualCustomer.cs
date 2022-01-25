namespace AW.Services.Sales.Core.Entities
{
    public class IndividualCustomer : Customer
    {
        public Person Person { get; set; } = new();

        public override string FullName => Person.FullName;
        public override CustomerType CustomerType => CustomerType.Individual;
    }
}