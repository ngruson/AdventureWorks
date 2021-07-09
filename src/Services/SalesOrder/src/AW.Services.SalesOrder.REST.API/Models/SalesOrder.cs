using AutoMapper;
using AW.Services.SalesOrder.Core.Entities;
using AW.SharedKernel.AutoMapper;
using System;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesOrder : IMapFrom<Core.Handlers.GetSalesOrders.SalesOrderDto>
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
            profile.CreateMap<Core.Handlers.GetSalesOrders.SalesOrderDto, SalesOrder>();
            profile.CreateMap<Core.Handlers.GetSalesOrdersForCustomer.SalesOrderDto, SalesOrder>()
                .ForMember(m => m.BillToAddress, opt => opt.Ignore())
                .ForMember(m => m.ShipToAddress, opt => opt.Ignore())
                .ForMember(m => m.CreditCard, opt => opt.Ignore())
                .ForMember(m => m.OrderLines, opt => opt.Ignore())
                .ForMember(m => m.SalesReasons, opt => opt.Ignore());
            profile.CreateMap<Core.Handlers.GetSalesOrder.SalesOrderDto, SalesOrder>();
        }
    }
}