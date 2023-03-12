using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
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

        public static string NullOrEmpty(this IGuardClause guardClause, string? input, ILogger logger, string? message = null, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                var ex = new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }

            return input;
        }
    }
}
