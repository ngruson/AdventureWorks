namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersDto
    {
        public GetSalesOrdersDto(List<SalesOrderDto> salesOrders, int totalSalesOrders)
        {
            SalesOrders = salesOrders;
            TotalSalesOrders = totalSalesOrders;
        }

        public List<SalesOrderDto> SalesOrders { get; private init; }
        public int TotalSalesOrders { get; private init; }
    }
}