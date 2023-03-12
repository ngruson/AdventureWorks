using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class SalesOrder : IMapFrom<Entities.SalesOrder>
    {
        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string? Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string? SalesOrderNumber { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? AccountNumber { get; set; }
        public Customer? Customer { get; set; }

        public SalesPerson? SalesPerson { get; set; }

        public string? Territory { get; set; }

        public Address? BillToAddress { get; set; }

        public Address? ShipToAddress { get; set; }

        public string? ShipMethod { get; set; }

        public CreditCard? CreditCard { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string? Comment { get; set; }

        public List<SalesOrderLine>? OrderLines { get; set; }

        public List<SalesReason>? SalesReasons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesOrder, SalesOrder>()
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.Status!.Name))
                .ForMember(m => m.SalesReasons, opt => opt.MapFrom(src => src.SalesReasons
                    .Select(r => r.SalesReason))
                );
        }
    }
}
