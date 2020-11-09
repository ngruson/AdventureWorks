using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;
using AW.StateProvinceService.Messages.ListStateProvinces;
using System.Reflection;

namespace AW.CustomerService
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<StateProvinceDto, StateProvince>();
        }
    }
}