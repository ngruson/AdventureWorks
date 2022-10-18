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

        public static void EmployeeNull(this IGuardClause guardClause, Entities.Employee? employee, string loginID, ILogger logger)
        {
            if (employee == null)
            {
                var ex = new EmployeeNotFoundException(loginID);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}