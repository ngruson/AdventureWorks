using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
{
    public class SalesOrderDto : IMapFrom<SalesOrderHeader>
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

        public AddressDto BillToAddress { get; set; }
        public AddressDto ShipToAddress { get; set; }

        public ShipMethodDto ShipMethod { get; set; }

        public List<SalesOrderLineDto> OrderLines { get; set; }

        public List<SalesReasonDto> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderHeader, SalesOrderDto>()
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(src => src.Customer.Name()))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => src.Customer.GetCustomerType()))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => src.SalesPerson != null ? src.SalesPerson.FullName : null))
                .ForMember(m => m.Territory, opt => opt.MapFrom(src => src.SalesTerritory != null ? src.SalesTerritory.Name : null))
                .ForMember(m => m.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
                .ForMember(m => m.SalesReasons, opt => opt.MapFrom(src => src.SalesReasons
                    .Select(r => r.SalesReason)));
        }
    }
}