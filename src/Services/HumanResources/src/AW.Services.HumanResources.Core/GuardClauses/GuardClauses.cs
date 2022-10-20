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

        public static void ShiftsNull(this IGuardClause guardClause, List<Entities.Shift> shifts, ILogger logger)
        {
            if (shifts == null)
            {
                var ex = new ShiftsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ShiftNull(this IGuardClause guardClause, Entities.Shift? shift, string name, ILogger logger)
        {
            if (shift == null)
            {
                var ex = new ShiftNotFoundException(name);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void DepartmentsNull(this IGuardClause guardClause, List<Entities.Department> departments, ILogger logger)
        {
            if (departments == null)
            {
                var ex = new DepartmentsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void DepartmentNull(this IGuardClause guardClause, Entities.Department? department, string name, ILogger logger)
        {
            if (department == null)
            {
                var ex = new DepartmentNotFoundException(name);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}