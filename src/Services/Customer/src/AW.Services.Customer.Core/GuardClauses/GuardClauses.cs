using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static void CustomersNullOrEmpty(this IGuardClause guardClause, List<Entities.Customer> customers, ILogger logger)
        {
            if (customers == null || customers.Count == 0)
            {
                var ex = new CustomersNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void CustomerNull(this IGuardClause guardClause, Entities.Customer? customer, string accountNumber, ILogger logger)
        {
            if (customer == null)
            {
                var ex = new CustomerNotFoundException(accountNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void AddressNull(this IGuardClause guardClause, Entities.CustomerAddress? address, string accountNumber, string addressType, ILogger logger)
        {
            if (address == null)
            {
                var ex = new AddressNotFoundException(accountNumber, addressType);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void EmailAddressNull(this IGuardClause guardClause, Entities.PersonEmailAddress? emailAddress, string accountNumber, string value, ILogger logger)
        {
            if (emailAddress == null)
            {
                var ex = new EmailAddressNotFoundException(accountNumber, value);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void PhoneNumberNull(this IGuardClause guardClause, Entities.PersonPhone? phone, string accountNumber, string phoneNumber, string phoneNumberType, ILogger logger)
        {
            if (phone == null)
            {
                var ex = new PhoneNumberNotFoundException(accountNumber, phoneNumber, phoneNumberType);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void StoreContactNull(this IGuardClause guardClause, Entities.StoreCustomerContact? contact, string accountNumber, string? contactName, string contactType, ILogger logger)
        {
            if (contact == null)
            {
                var ex = new StoreContactNotFoundException(accountNumber, contactName, contactType);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}