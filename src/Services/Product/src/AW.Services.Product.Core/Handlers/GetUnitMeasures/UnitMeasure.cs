using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetUnitMeasures
{
    public class UnitMeasure : IMapFrom<Entities.UnitMeasure>
    {
        public string? UnitMeasureCode { get; set; }
        public string? Name { get; set; }
    }
}
