using Ardalis.GuardClauses;
using AW.Services.HumanResources.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static void EmployeesNull(this IGuardClause guardClause, List<Entities.Employee> employees, ILogger logger)
        {
            if (employees == null)
            {
                var ex = new EmployeesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}