namespace AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders
{
    public class ShipMethod
    {
        public string Name { get; set; }

        public decimal ShipBase { get; set; }

        public decimal ShipRate { get; set; }
    }
}