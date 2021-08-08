namespace AW.Services.Customer.Core.Entities
{
    public class IndividualCustomer : Customer
    {
        public Person Person { get; set; } = new Person();

        public override CustomerType CustomerType => CustomerType.Individual;
    }
}