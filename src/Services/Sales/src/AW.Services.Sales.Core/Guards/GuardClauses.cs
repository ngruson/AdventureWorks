using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.Guards
{
    public static class GuardClauses
    {
        public static void SalesOrderNull(this IGuardClause guardClause, Entities.SalesOrder input)
        {
            if (input == null)
                throw new SalesOrderNotFoundException();
        }

        public static void CustomerNull(this IGuardClause guardClause, Entities.Customer input, string customerNumber, ILogger logger)
        {
            if (input == null)
            {
                logger.LogInformation("Customer {CustomerNumber} not found", customerNumber);
                throw new CustomerNotFoundException();
            }
                
        }
    }
}