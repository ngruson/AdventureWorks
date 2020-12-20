using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using System;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class SalesOrderViewModel : IMapFrom<GetCustomer.SalesOrder>
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Status { get; set; }

        public string OnlineOrdered { get; set; }

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
            profile.CreateMap<GetCustomer.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => src.OnlineOrderFlag ? "Yes" : "No"));
            profile.CreateMap<ListCustomers.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => src.OnlineOrderFlag ? "Yes" : "No"));
        }
    }
}