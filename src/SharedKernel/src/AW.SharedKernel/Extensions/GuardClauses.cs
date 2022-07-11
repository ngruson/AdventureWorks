using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using System;

namespace AW.SharedKernel.Extensions
{
    public static class GuardClauses
    {
        public static T Null<T>(this IGuardClause guardClause, T input, string parameterName, ILogger logger)
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