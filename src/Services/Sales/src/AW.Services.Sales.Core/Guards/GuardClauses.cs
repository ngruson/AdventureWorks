using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Exceptions;

namespace AW.Services.Sales.Core.Guards
{
    public static class GuardClauses
    {
        public static void SalesOrderNull(this IGuardClause guardClause, Entities.SalesOrder input)
        {
            if (input == null)
                throw new SalesOrderNotFoundException();
        }
    }
}