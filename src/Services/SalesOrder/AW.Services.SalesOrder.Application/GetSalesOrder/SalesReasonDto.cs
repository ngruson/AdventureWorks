using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
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