using AutoMapper;
using AW.Services.SalesOrder.Core.Entities;
using AW.SharedKernel.AutoMapper;
using System;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer
{
    public class SalesOrderDto : IMapFrom<Entities.SalesOrder>
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
        public string CustomerNumber { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public string ShipMethod { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string Comment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesOrder, SalesOrderDto>();
        }
    }
}