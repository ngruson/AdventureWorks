﻿namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public class CreditCard
    {
        public string? CardType { get; set; }

        public string? CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }
    }
}