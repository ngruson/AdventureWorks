﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using AW.Services.HumanResources.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static Result EmployeesNullOrEmpty(this IGuardClause guardClause, List<Entities.Employee> employees, ILogger logger)
        {
            if (employees == null || employees.Count == 0)
            {
                var ex = new EmployeesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result EmployeeNull(this IGuardClause guardClause, Entities.Employee? employee, Guid objectId, ILogger logger)
        {
            if (employee == null)
            {
                var ex = new EmployeeNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result EmployeeDepartmentHistoryNull(this IGuardClause guardClause, Entities.EmployeeDepartmentHistory? edh,
            Guid objectId,
            ILogger logger
        )
        {
            if (edh == null)
            {
                var ex = new EmployeeDepartmentHistoryNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result ShiftsNull(this IGuardClause guardClause, List<Entities.Shift> shifts, ILogger logger)
        {
            if (shifts == null)
            {
                var ex = new ShiftsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result ShiftNull(this IGuardClause guardClause, Entities.Shift? shift, Guid objectId, ILogger logger)
        {
            if (shift == null)
            {
                var ex = new ShiftNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result DepartmentsNull(this IGuardClause guardClause, List<Entities.Department> departments, ILogger logger)
        {
            if (departments == null)
            {
                var ex = new DepartmentsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }

        public static Result DepartmentNull(this IGuardClause guardClause, Entities.Department? department, Guid objectId, ILogger logger)
        {
            if (department == null)
            {
                var ex = new DepartmentNotFoundException(objectId);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.NotFound(ex.Message);
            }

            return Result.Success();
        }
    }
}
