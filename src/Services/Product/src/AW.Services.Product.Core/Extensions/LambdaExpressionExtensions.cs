using System;
using System.Linq.Expressions;

namespace AW.Services.Product.Core.Extensions
{
    public static class LambdaExpressionExtensions
    {
        public static Expression<Func<TInput, object>> ToUntypedPropertyExpression<TInput>(this LambdaExpression expression)
        {
            var converted = Expression.Convert(
                expression.Body, typeof(object)
            );

            return Expression.Lambda<Func<TInput, object>>(converted, expression.Parameters);
        }
    }
}