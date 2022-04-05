using Ardalis.SmartEnum;

namespace AW.Services.Sales.Core.Entities
{
    public sealed class SalesOrderStatus : SmartEnum<SalesOrderStatus>
    {
        public static readonly SalesOrderStatus InProcess = new SalesOrderStatus(nameof(InProcess), 1);
        public static readonly SalesOrderStatus Approved = new SalesOrderStatus(nameof(Approved), 2);
        public static readonly SalesOrderStatus Backordered = new SalesOrderStatus(nameof(Backordered), 3);
        public static readonly SalesOrderStatus Rejected = new SalesOrderStatus(nameof(Rejected), 4);
        public static readonly SalesOrderStatus Shipped = new SalesOrderStatus(nameof(Shipped), 5);
        public static readonly SalesOrderStatus Cancelled = new SalesOrderStatus(nameof(Cancelled), 6);

        private SalesOrderStatus(string name, int value) : base(name, value) { }
    }
}