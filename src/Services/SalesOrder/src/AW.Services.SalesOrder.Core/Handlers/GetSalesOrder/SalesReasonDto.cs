using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.SalesOrder.Core.Entities;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrder
{
    public class SalesReasonDto : IMapFrom<SalesReason>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReason, SalesReasonDto>();
        }
    }
}