using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class SalesOrderViewModel : IMapFrom<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrder>
    {
        [Display(Name = "Revision number")]
        public string RevisionNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ship date")]
        [DisplayFormat(DataFormatString = "{0:d}", ConvertEmptyStringToNull = true)]
        public DateTime? ShipDate { get; set; }

        public SalesOrderStatus Status { get; set; }

        [Display(Name = "Ordered online")]
        public bool OnlineOrderFlag { get; set; }

        [Display(Name = "Sales order number")]
        public string SalesOrderNumber { get; set; }

        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNumber { get; set; }

        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }

        public CustomerViewModel Customer { get; set; }

        [Display(Name = "Sales person")]
        [Required]
        public string SalesPerson { get; set; }

        [Display(Name = "Sales territory")]
        [Required]
        public string Territory { get; set; }

        [Display(Name = "Subtotal")]
        [DisplayFormat(DataFormatString = "${0:C}")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Tax amount")]
        [DisplayFormat(DataFormatString = "${0:C}")]
        public decimal TaxAmt { get; set; }

        [Display(Name = "Freight")]
        [DisplayFormat(DataFormatString = "${0:C}")]
        public decimal Freight { get; set; }

        [Display(Name = "Total due")]
        [DisplayFormat(DataFormatString = "${0:C}")]
        public decimal TotalDue { get; set; }

        public AddressViewModel BillToAddress { get; set; }

        public AddressViewModel ShipToAddress { get; set; }

        [Display(Name = "Shipping method")]
        public string ShipMethod { get; set; }

        public CreditCardViewModel CreditCard { get; set; }

        public List<SalesOrderLineViewModel> OrderLines { get; set; }

        public List<SalesReasonViewModel> SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.Status, opt => opt.MapFrom(src => SalesOrderStatus.FromName(src.Status.ToString(), false)))
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => MapSalesPerson(src.SalesPerson.Name.FullName)))
                .ReverseMap();

            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrder.SalesOrder, SalesOrderViewModel>()
                .ForMember(m => m.Status, opt => opt.MapFrom(src => SalesOrderStatus.FromName(src.Status.ToString(), false)))
                .ForMember(m => m.RevisionNumber, opt => opt.MapFrom(src => src.RevisionNumber.ToString()))
                .ForMember(m => m.PurchaseOrderNumber, opt => opt.MapFrom(src => MapPurchaseOrderNumber(src.PurchaseOrderNumber)))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => MapSalesPerson(src.SalesPerson.Name.FullName)))
                .ReverseMap();
        }

        private static string MapPurchaseOrderNumber(string purchaseOrderNumber)
        {
            if (!string.IsNullOrEmpty(purchaseOrderNumber))
                return purchaseOrderNumber;

            return "-";
        }

        private static string MapSalesPerson(string salesPerson)
        {
            if (!string.IsNullOrEmpty(salesPerson))
                return salesPerson;

            return "-";
        }
    }
}