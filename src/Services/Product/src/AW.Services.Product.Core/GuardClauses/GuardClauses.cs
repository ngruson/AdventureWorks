using Ardalis.GuardClauses;
using AW.Services.Product.Core.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AW.Services.Product.Core.GuardClauses
{
    public static class GuardClauses
    {
        public static void ProductsNull(this IGuardClause guardClause, List<Entities.Product> products, ILogger logger)
        {
            if (products == null)
            {
                var ex = new ProductsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ProductNull(this IGuardClause guardClause, Entities.Product product, string productNumber, ILogger logger)
        {
            if (product == null)
            {
                var ex = new ProductNotFoundException(productNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void ProductCategoriesNull(this IGuardClause guardClause, List<Entities.ProductCategory> categories, ILogger logger)
        {
            if (categories == null)
            {
                var ex = new ProductCategoriesNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}