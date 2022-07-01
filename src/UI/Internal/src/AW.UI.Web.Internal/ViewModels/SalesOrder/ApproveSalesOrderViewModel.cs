using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class ApproveSalesOrderViewModel : IMapFrom<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrder>
    {
        [Display(Name = "Sales order number")]
        public string SalesOrderNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer type")]
        public string CustomerType { get; set; }

        [Display(Name = "Ordered online")]
        public string OnlineOrdered { get; set; }

        [Display(Name = "Shipping method")]
        public string ShipMethod { get; set; }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.SalesOrder.Handlers.GetSalesOrders.SalesOrder, ApproveSalesOrderViewModel>()
                .ForMember(m => m.OnlineOrdered, opt => opt.MapFrom(src => MapOnlineOrderFlag(src.OnlineOrderFlag)))
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => MapSalesPerson(src.SalesPerson.Name.FullName)))
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => src.Customer.CustomerType))
                .ReverseMap();
        }        

        private static string MapOnlineOrderFlag(bool onlineOrderFlag)
        {
            return onlineOrderFlag ? "Yes" : "No";
        }

        private static string MapSalesPerson(string salesPerson)
        {
            if (!string.IsNullOrEmpty(salesPerson))
                return salesPerson;

            return "-";
        }
    }
}