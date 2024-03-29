﻿namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder
{
    public class SalesOrderLine
    {
        public string? CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }
        public string? SpecialOfferDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public byte[]? ThumbNailPhoto { get; set; }
        public string? Color { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
    }
}
