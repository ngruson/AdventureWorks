using AW.Core.Application.AutoMapper;
using AW.CustomerService.Messages.UpdateCustomer;
using System.Reflection;

namespace AW.CustomerService
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            //CreateMap<UpdateCustomer, Application.Customer.UpdateCustomer.CustomerDto>();
            //CreateMap<UpdateStore, Application.Customer.UpdateCustomer.StoreCustomerDto>();
            //CreateMap<UpdatePerson, Application.Customer.UpdateCustomer.PersonCustomerDto>();
        }
    }
}