using AutoMapper;
using AW.Application.AutoMapper;
using System;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesOrderViewModel : IMapFrom<SalesOrderService.SalesOrder>
    {
        public string RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Status { get; set; }

        public string OnlineOrdered { get; set; }

        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public AddressViewModel BillToAddress { get; set; }

        public AddressViewModel ShipToAddress { get; set; }

        public ShipMethodViewModel ShipMethod { get; set; }

        public List<SalesOrderLineViewModel> OrderLines { get; set; }

        public List<SalesReasonViewModel> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderService.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => src.OnlineOrderFlag ? "Yes" : "No"))
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.PurchaseOrderNumber) ? src.PurchaseOrderNumber : "-"))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.SalesPerson) ? src.SalesPerson : "-"));

            profile.CreateMap<SalesOrderService.SalesOrder1, SalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => src.OnlineOrderFlag ? "Yes" : "No"))
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.PurchaseOrderNumber) ? src.PurchaseOrderNumber : "-"))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.SalesPerson) ? src.SalesPerson : "-"));
        }
    }
}