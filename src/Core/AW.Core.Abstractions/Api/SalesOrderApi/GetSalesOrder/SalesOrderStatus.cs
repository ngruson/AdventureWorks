﻿namespace AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder
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