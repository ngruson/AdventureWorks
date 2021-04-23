using AutoMapper;
using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomer;
using AW.UI.Web.Internal.Common;
using System;
using m = AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class SalesOrderViewModel : IMapFrom<m.SalesOrder>
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
            profile.CreateMap<m.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)));
        }

        private string MapPurchaseOrderNumber(string purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
                return purchaseOrderNumber;

            return "-";
        }
    }
}