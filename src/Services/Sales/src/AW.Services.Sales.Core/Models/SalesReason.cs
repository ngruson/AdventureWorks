using AutoMapper;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class SalesReason : IMapFrom<Handlers.GetSalesOrders.SalesReasonDto>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.SalesReasonDto, SalesReason>();
            profile.CreateMap<SalesReasonDto, SalesReason>();
            profile.CreateMap<SalesReason, Handlers.UpdateSalesOrder.SalesReasonDto>()
                .ReverseMap();
        }
    }
}