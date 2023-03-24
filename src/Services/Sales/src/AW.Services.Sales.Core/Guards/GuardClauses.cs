using Ardalis.GuardClauses;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.Guards
{
    public static class GuardClauses
    {
        public static void SalesOrdersNull(this IGuardClause guardClause, List<Entities.SalesOrder> input, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesOrdersNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SalesOrderNull(this IGuardClause guardClause, Entities.SalesOrder? input, string salesOrderNumber, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesOrderNotFoundException(salesOrderNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SalesOrderNull(this IGuardClause guardClause, SalesOrder? input, string salesOrderNumber, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesOrderNotFoundException(salesOrderNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SalesOrderLineNull(this IGuardClause guardClause, Entities.SalesOrderLine? input, string productNumber, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesOrderLineNotFoundException(productNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void CustomerNull(this IGuardClause guardClause, Entities.Customer? input, string customerNumber, ILogger logger)
        {
            if (input == null)
            {
                var ex = new CustomerNotFoundException(customerNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SpecialOfferProductNull(this IGuardClause guardClause, Entities.SpecialOfferProduct? input, string productNumber, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SpecialOfferProductNotFoundException(productNumber);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SalesPersonsNull(this IGuardClause guardClause, List<Entities.SalesPerson> input, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesPersonsNotFoundException();
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }

        public static void SalesPersonNull(this IGuardClause guardClause, Entities.SalesPerson? input, string name, ILogger logger)
        {
            if (input == null)
            {
                var ex = new SalesPersonNotFoundException(name);
                logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw ex;
            }
        }
    }
}
