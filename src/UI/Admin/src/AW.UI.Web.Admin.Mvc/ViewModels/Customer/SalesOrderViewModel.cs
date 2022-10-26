using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using System;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class SalesOrderViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomer.SalesOrder>
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
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomer.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)));

            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)));
        }

        private static string MapPurchaseOrderNumber(string purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
                return purchaseOrderNumber;

            return "-";
        }
    }
}