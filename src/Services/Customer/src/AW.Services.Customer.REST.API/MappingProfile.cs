using System.Reflection;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Core.Handlers.GetCustomers.CustomerDto, Models.GetCustomers.Customer>()
                .Include<Core.Handlers.GetCustomers.IndividualCustomerDto, Models.GetCustomers.IndividualCustomer>()
                .Include<Core.Handlers.GetCustomers.StoreCustomerDto, Models.GetCustomers.StoreCustomer>();

            CreateMap<Core.Handlers.GetCustomer.CustomerDto, Models.GetCustomer.Customer>()
                .Include<Core.Handlers.GetCustomer.IndividualCustomerDto, Models.GetCustomer.IndividualCustomer>()
                .Include<Core.Handlers.GetCustomer.StoreCustomerDto, Models.GetCustomer.StoreCustomer>();

            CreateMap<Models.UpdateCustomer.Customer, Core.Handlers.UpdateCustomer.UpdateCustomerCommand>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src));

            CreateMap<Models.UpdateCustomer.Customer, Core.Handlers.UpdateCustomer.CustomerDto>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .Include<Models.UpdateCustomer.IndividualCustomer, Core.Handlers.UpdateCustomer.IndividualCustomerDto>()
                .Include<Models.UpdateCustomer.StoreCustomer, Core.Handlers.UpdateCustomer.StoreCustomerDto>()
                .ReverseMap()
                .Include<Core.Handlers.UpdateCustomer.IndividualCustomerDto, Models.UpdateCustomer.IndividualCustomer>()
                .Include<Core.Handlers.UpdateCustomer.StoreCustomerDto, Models.UpdateCustomer.StoreCustomer>();
        }
    }
}