using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
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
        public string CustomerName { get; set; }

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
            profile.CreateMap<Domain.SalesOrder, SalesOrderDto>()
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(m => m.SalesReasons, opt => opt.MapFrom(src => src.SalesReasons
                    .Select(r => r.SalesReason)));
        }
    }
}