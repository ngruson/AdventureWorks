using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Exceptions;

namespace AW.Services.Customer.Core.GuardClauses
{
    public static class CustomerGuard
    {
        public static void CustomerNull(this IGuardClause guardClause, Entities.Customer customer, string accountNumber)
        {
            if (customer == null)
                throw new CustomerNotFoundException(accountNumber);
        }
    }
}