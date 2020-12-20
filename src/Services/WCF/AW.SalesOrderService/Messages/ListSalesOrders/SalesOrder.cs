using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrders;
using System;
using System.Collections.Generic;

namespace AW.SalesOrderService.Messages.ListSalesOrders
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

        public string AccountNumber { get; set; }

        public string CustomerName { get; set; }

        public CustomerType CustomerType { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public Address BillToAddress { get; set; }
        public Address ShipToAddress { get; set; }

        public ShipMethod ShipMethod { get; set; }

        public List<SalesOrderLine> OrderLines { get; set; }

        public List<SalesReason> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderDto, SalesOrder>();
        }
    }
}