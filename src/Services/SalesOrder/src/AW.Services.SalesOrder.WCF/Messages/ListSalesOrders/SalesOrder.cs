using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;
using System;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    public class SalesOrder : IMapFrom<SalesOrderDto>
    {
        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }
        public string CustomerNumber { get; set; }

        public string AccountNumber { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public Address BillToAddress { get; set; }

        public Address ShipToAddress { get; set; }

        public string ShipMethod { get; set; }

        public CreditCard CreditCard { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string Comment { get; set; }

        public List<SalesOrderLine> OrderLines { get; set; }

        public List<SalesReason> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderDto, SalesOrder>();
        }
    }
}