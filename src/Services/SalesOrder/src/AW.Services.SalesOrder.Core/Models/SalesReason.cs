using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.Core.Models
{
    public class SalesReason : IMapFrom<Handlers.GetSalesOrders.SalesReasonDto>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.SalesReasonDto, SalesReason>();
            profile.CreateMap<Handlers.GetSalesOrder.SalesReasonDto, SalesReason>();
        }
    }
}