namespace AW.Services.SalesOrder.Domain
{
    public class IndividualCustomer : Customer
    {
        public Person Person { get; set; } = new Person();
        public override string CustomerName => Person.FullName();
    }
}