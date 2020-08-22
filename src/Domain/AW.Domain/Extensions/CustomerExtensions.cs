namespace AW.Domain.Sales
{
    public static class CustomerExtensions
    {

        public static string Name(this Customer customer)
        {
            if (customer.Person != null)
                return customer.Person.FullName;
            else if (customer.Store != null)
                return customer.Store.Name;

            return null;
        }

        public static CustomerType GetCustomerType(this Customer customer)
        {
            return customer.Person != null && customer.Store == null ? CustomerType.Individual :
                customer.Store != null ? CustomerType.Store : default;
        }
    }
}