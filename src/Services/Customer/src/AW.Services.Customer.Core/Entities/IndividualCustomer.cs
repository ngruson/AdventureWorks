using AW.Services.Customer.Core.Entities.PreferredAddress;

namespace AW.Services.Customer.Core.Entities
{
    public class IndividualCustomer : Customer
    {
        public IndividualCustomer()
        {
            preferredAddressStrategy = new IndividualPreferredAddressStrategy(this);
        }

        public Person Person { get; set; } = new Person();

        public override CustomerType CustomerType => CustomerType.Individual;
    }
}