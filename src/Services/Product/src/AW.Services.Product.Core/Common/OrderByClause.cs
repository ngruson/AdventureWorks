using System;
using System.Linq.Expressions;

namespace AW.Services.Product.Core.Common
{
    public class OrderByClause<T>
    {
        public OrderByClause(Expression<Func<T, object>> expression, OrderByDirection direction)
        {
            Expression = expression;
            Direction = direction;
        }

        public Expression<Func<T, object>> Expression { get; set; }
        public OrderByDirection Direction { get; set; }
    }
}