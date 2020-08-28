using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;

namespace AW.Application.SalesOrder
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