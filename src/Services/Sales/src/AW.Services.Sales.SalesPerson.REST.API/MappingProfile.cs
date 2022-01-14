using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.Sales.SalesPerson.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}