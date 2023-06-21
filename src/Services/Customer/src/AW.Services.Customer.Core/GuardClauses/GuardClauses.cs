using Ardalis.GuardClauses;
using Ardalis.Result;
using AW.Services.Customer.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static Result CustomersNullOrEmpty(this IGuardClause guardClause, List<Entities.Customer> customers, ILogger logger)
        {
            if (customers == null || customers.Count == 0)
            {
                var ex = new CustomersNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result CustomerNull(this IGuardClause guardClause, Entities.Customer? customer, Guid objectId, ILogger logger)
        {
            if (customer == null)
            {
                var ex = new CustomerNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result AddressNull(this IGuardClause guardClause, Entities.CustomerAddress? address, Guid objectId, ILogger logger)
        {
            if (address == null)
            {
                var ex = new AddressNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result EmailAddressNull(this IGuardClause guardClause, Entities.PersonEmailAddress? emailAddress, Guid objectId, ILogger logger)
        {
            if (emailAddress == null)
            {
                var ex = new EmailAddressNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result PhoneNumberNull(this IGuardClause guardClause, Entities.PersonPhone? phone, Guid objectId, ILogger logger)
        {
            if (phone == null)
            {
                var ex = new PhoneNumberNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result StoreContactNull(this IGuardClause guardClause, Entities.StoreCustomerContact? contact, Guid objectId, ILogger logger)
        {
            if (contact == null)
            {
                var ex = new StoreContactNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }
    }
}
