using System.Linq;

namespace AW.Services.Customer.Core.Entities.PreferredAddress
{
    public class StorePreferredAddressStrategy : IPreferredAddressStrategy
    {
        private readonly Customer customer;

        public StorePreferredAddressStrategy(
            Customer customer
        ) =>
            this.customer = customer;

        public Address GetPreferredAddress(string addressType)
        {
            if (addressType == "Billing")
                return GetPreferredAddressForBilling();
            else if (addressType == "Shipping")
                return GetPreferredAddressForShipping();

            return null;
        }

        private Address GetPreferredAddressForBilling()
        {
            var customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Billing");
            if (customerAddress != null)
                return customerAddress.Address;

            customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Main Office");
            if (customerAddress != null)
                return customerAddress.Address;

            customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Home");
            if (customerAddress != null)
                return customerAddress.Address;

            return null;
        }

        private Address GetPreferredAddressForShipping()
        {
            var customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Shipping");
            if (customerAddress != null)
                return customerAddress.Address;

            customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Main Office");
            if (customerAddress != null)
                return customerAddress.Address;

            customerAddress = customer.Addresses.SingleOrDefault(_ => _.AddressType == "Home");
            if (customerAddress != null)
                return customerAddress.Address;

            return null;
        }
    }
}