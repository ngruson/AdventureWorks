using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetLocations
{
    public class Location : IMapFrom<Entities.Location>
    {
        public string? Name { get; set; }

        public decimal CostRate { get; set; }

        public decimal Availability { get; set; }
    }
}
