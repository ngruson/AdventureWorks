using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders
{
    public class CustomerBuilder
    {
        private Customer customer = new Customer();

        public CustomerBuilder AccountNumber(string accountNumber)
        {
            customer.AccountNumber = accountNumber;
            return this;
        }

        public Customer Build()
        {
            return customer;
        }

        public CustomerBuilder WithTestValues()
        {
            customer = new Customer
            {
                AccountNumber = "AW00000001"
            };

            return this;
        }
    }
}
