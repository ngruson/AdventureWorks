using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AutoMapper.EquivalencyExpression;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class SalesReason : IMapFrom<Entities.SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderSalesReason, SalesReason>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.SalesReason!.Name))
                .ForMember(m => m.ReasonType, opt => opt.MapFrom(src => src.SalesReason!.ReasonType))
                .ReverseMap()
                .EqualityComparison((salesReasonDto, salesReason) => salesReasonDto.Name == salesReason.SalesReason!.Name);

            profile.CreateMap<Entities.SalesReason, SalesReason>()
                .ReverseMap();
        }
    }
}
