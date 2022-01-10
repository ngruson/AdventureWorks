using Ardalis.GuardClauses;
using AW.Services.SalesOrder.Core.Exceptions;

namespace AW.Services.SalesOrder.Core.Guards
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