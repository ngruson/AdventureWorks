using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;
using System;

namespace AW.Application.Customer
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

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderHeader, SalesOrderDto>()
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => src.SalesPerson != null ? src.SalesPerson.FullName : null))
                .ForMember(m => m.Territory, opt => opt.MapFrom(src => src.SalesTerritory != null ? src.SalesTerritory.Name : null));
        }
    }
}