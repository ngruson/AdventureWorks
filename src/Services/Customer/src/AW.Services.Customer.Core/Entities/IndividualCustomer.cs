using AW.Services.Customer.Core.Entities.PreferredAddress;

namespace AW.Services.Customer.Core.Entities
{
    public class IndividualCustomer : Customer
    {
        public IndividualCustomer(string accountNumber) : base(accountNumber) { }
        public IndividualCustomer()
        {
            preferredAddressFactory = new IndividualPreferredAddressFactory(this);
        }
        public IndividualCustomer(Person person) : base()
        {
            Person = person;
        }

        public Person Person { get; private set; }

        public override CustomerType CustomerType => CustomerType.Individual;
    }
}