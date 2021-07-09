using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(Handlers.GetCustomers.GetCustomersQuery).Assembly);

            CreateMap<Entities.Customer, Handlers.GetCustomers.CustomerDto>()
                .Include<Entities.IndividualCustomer, Handlers.GetCustomers.IndividualCustomerDto>()
                .Include<Entities.StoreCustomer, Handlers.GetCustomers.StoreCustomerDto>();

            CreateMap<Entities.Customer, Handlers.GetCustomer.CustomerDto>()
                .Include<Entities.IndividualCustomer, Handlers.GetCustomer.IndividualCustomerDto>()
                .Include<Entities.StoreCustomer, Handlers.GetCustomer.StoreCustomerDto>();

            CreateMap<Handlers.AddCustomer.CustomerDto, Entities.Customer>()
                .Include<Handlers.AddCustomer.IndividualCustomerDto, Entities.IndividualCustomer>()
                .Include<Handlers.AddCustomer.StoreCustomerDto, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Handlers.UpdateCustomer.CustomerDto, Entities.Customer>()
                .Include<Handlers.UpdateCustomer.IndividualCustomerDto, Entities.IndividualCustomer>()
                .Include<Handlers.UpdateCustomer.StoreCustomerDto, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}