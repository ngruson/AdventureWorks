using AutoMapper;
using AW.UI.Web.Internal.Common;
using System;
using System.Collections.Generic;
using s = AW.UI.Web.Internal.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class SalesOrderViewModel : IMapFrom<s.SalesOrder>
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

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public AddressViewModel BillToAddress { get; set; }

        public AddressViewModel ShipToAddress { get; set; }

        public string ShipMethod { get; set; }

        public List<SalesOrderLineViewModel> OrderLines { get; set; }

        public List<SalesReasonViewModel> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<s.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => MapOnlineOrderFlag(src.OnlineOrderFlag)))
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => MapSalesPerson(src.SalesPerson)));
        }

        private string MapOnlineOrderFlag(bool onlineOrderFlag)
        {
            if (onlineOrderFlag)
                return "Yes";

            return "No";
        }

        private string MapPurchaseOrderNumber(string purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
                return purchaseOrderNumber;

            return "-";
        }

        private string MapSalesPerson(string salesPerson)
        {
            if (!string.IsNullOrEmpty(salesPerson))
                return salesPerson;

            return "-";
        }
    }
}