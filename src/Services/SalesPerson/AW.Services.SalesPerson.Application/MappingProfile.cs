using AW.Common.AutoMapper;
using AW.Services.SalesPerson.Application.GetSalesPersons;

namespace AW.Services.SalesPerson.Application
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetSalesPersonsQuery).Assembly);
        }
    }
}