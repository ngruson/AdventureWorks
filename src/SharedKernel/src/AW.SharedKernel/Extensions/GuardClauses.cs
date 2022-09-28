using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace AW.SharedKernel.Extensions
{
    public static class GuardClauses
    {
        public static T Null<T>(this IGuardClause guardClause, T input, ILogger logger, [CallerArgumentExpression("input")]  string? parameterName = null)
        {
            if (input is null)
            {
                var ex = new ArgumentNullException(parameterName);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }

            return input;
        }
    }
}