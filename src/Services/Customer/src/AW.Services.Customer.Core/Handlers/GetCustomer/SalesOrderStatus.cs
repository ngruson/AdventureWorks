namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public enum SalesOrderStatus : byte
    {
        InProcess = 1,
        Approved = 2,
        Backordered = 3,
        Rejected = 4,
        Shipped = 5,
        Cancelled = 6
    }
}