using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.Core
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetSalesPersonsQuery).Assembly);
        }
    }
}