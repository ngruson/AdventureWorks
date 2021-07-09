using System;
using System.Linq.Expressions;

namespace AW.Services.Product.Core.Common
{
    public class OrderByClause<T>
    {
        public Expression<Func<T, object>> Expression { get; set; }
        public OrderByDirection Direction { get; set; }
    }
}