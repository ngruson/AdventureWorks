using AutoMapper;
using AW.Common.AutoMapper;
using System;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class SalesOrderDto : IMapFrom<Domain.SalesOrder>
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }

        public decimal TotalDue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.SalesOrder, SalesOrderDto>();
        }
    }
}