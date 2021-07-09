using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.SalesOrder.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrders
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

        public AddressDto BillToAddress { get; set; }

        public AddressDto ShipToAddress { get; set; }

        public string ShipMethod { get; set; }

        public CreditCardDto CreditCard { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string Comment { get; set; }

        public List<SalesOrderLineDto> OrderLines { get; set; }

        public List<SalesReasonDto> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesOrder, SalesOrderDto>()
                .ForMember(m => m.SalesReasons, opt => opt.MapFrom(src => src.SalesReasons
                    .Select(r => r.SalesReason)));
        }
    }
}