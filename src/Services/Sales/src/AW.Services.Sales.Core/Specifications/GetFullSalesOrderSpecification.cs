using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetFullSalesOrderSpecification : Specification<Entities.SalesOrder>, ISingleResultSpecification<Entities.SalesOrder>
    {
        public GetFullSalesOrderSpecification(string salesOrderNumber) : base()
        {
            Query.Include(_ => _.Customer);
            Query.Include(_ => _.Customer.SalesOrders);
            Query.Include("Customer.Person");
            Query.Include(_ => _.CreditCard);
            Query.Include(_ => _.BillToAddress);
            Query.Include(_ => _.ShipToAddress);
            Query.Include(_ => _.OrderLines)
                .ThenInclude(_ => _.SpecialOfferProduct)
                    .ThenInclude(_ => _.SpecialOffer);
            Query.Include(_ => _.SalesPerson);
            Query.Include(_ => _.SalesReasons)
                .ThenInclude(_ => _.SalesReason);

            Query
                .Where(c => c.SalesOrderNumber == salesOrderNumber);
        }
    }
}