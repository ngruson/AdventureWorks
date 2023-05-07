using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;
using System;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class SalesOrderViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.SalesOrder>
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public Infrastructure.Api.Customer.Handlers.GetCustomers.SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string? SalesOrderNumber { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? AccountNumber { get; set; }

        public decimal TotalDue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.SalesOrder, SalesOrderViewModel>();

            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber!)));

            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.SalesOrder, SalesOrderViewModel>();

            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber!)));
        }

        private static string MapPurchaseOrderNumber(string purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
                return purchaseOrderNumber;

            return "-";
        }
    }
}
