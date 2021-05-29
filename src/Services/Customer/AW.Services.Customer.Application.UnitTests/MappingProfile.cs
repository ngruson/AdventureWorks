using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.Application.UnitTests
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetCustomersQuery).Assembly);

            CreateMap<Domain.Customer, GetCustomers.CustomerDto>()
                .Include<Domain.IndividualCustomer, GetCustomers.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, GetCustomers.StoreCustomerDto>();

            CreateMap<Domain.Customer, GetCustomer.CustomerDto>()
                .Include<Domain.IndividualCustomer, GetCustomer.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, GetCustomer.StoreCustomerDto>();

            CreateMap<AddCustomer.CustomerDto, Domain.Customer>()
                .Include<AddCustomer.IndividualCustomerDto, Domain.IndividualCustomer>()
                .Include<AddCustomer.StoreCustomerDto, Domain.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateCustomer.CustomerDto, Domain.Customer>()
                .Include<UpdateCustomer.IndividualCustomerDto, Domain.IndividualCustomer>()
                .Include<UpdateCustomer.StoreCustomerDto, Domain.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}