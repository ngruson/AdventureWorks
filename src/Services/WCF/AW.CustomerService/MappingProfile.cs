using AW.Application.AutoMapper;
using AW.Application.Customer;
using AW.CustomerService.Messages;
using AW.CustomerService.Messages.UpdateCustomer;
using System.Reflection;

namespace AW.CustomerService
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<UpdateCustomer, CustomerDto>();
            CreateMap<UpdateStore, StoreCustomerDto>();
            CreateMap<UpdatePerson, PersonCustomerDto>();
        }
    }
}