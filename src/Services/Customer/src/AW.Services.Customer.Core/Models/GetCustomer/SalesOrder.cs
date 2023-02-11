using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using System;

namespace AW.Services.Customer.Core.Models.GetCustomer
{
    public class SalesOrder : IMapFrom<SalesOrderDto>
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string? SalesOrderNumber { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? AccountNumber { get; set; }

        public decimal TotalDue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderDto, SalesOrder>();
        }
    }
}