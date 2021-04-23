namespace AW.Services.Customer.Domain
{
    public class IndividualCustomer : Customer
    {
        public Person Person { get; set; } = new Person();
        //public int PersonId;

        public override CustomerType CustomerType => CustomerType.Individual;
    }
}