using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Domain;
using System;

namespace AW.Services.SalesOrder.Application.GetSalesOrdersForCustomer
{
    public class SalesOrderDto : IMapFrom<Domain.SalesOrder>
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
            profile.CreateMap<Domain.SalesOrder, SalesOrderDto>();
        }
    }
}