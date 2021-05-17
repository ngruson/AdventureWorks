using app =AW.Services.Customer.Application;
using System.Reflection;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<app.GetCustomers.CustomerDto, Models.GetCustomers.Customer>()
                .Include<app.GetCustomers.IndividualCustomerDto, Models.GetCustomers.IndividualCustomer>()
                .Include<app.GetCustomers.StoreCustomerDto, Models.GetCustomers.StoreCustomer>();

            CreateMap<app.GetCustomer.CustomerDto, Models.GetCustomer.Customer>()
                .Include<app.GetCustomer.IndividualCustomerDto, Models.GetCustomer.IndividualCustomer>()
                .Include<app.GetCustomer.StoreCustomerDto, Models.GetCustomer.StoreCustomer>();

            CreateMap<Models.UpdateCustomer.Customer, app.UpdateCustomer.UpdateCustomerCommand>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src));

            CreateMap<Models.UpdateCustomer.Customer, app.UpdateCustomer.CustomerDto>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .Include<Models.UpdateCustomer.IndividualCustomer, app.UpdateCustomer.IndividualCustomerDto>()
                .Include<Models.UpdateCustomer.StoreCustomer, app.UpdateCustomer.StoreCustomerDto>()
                .ReverseMap()
                .Include<app.UpdateCustomer.IndividualCustomerDto, Models.UpdateCustomer.IndividualCustomer>()
                .Include<app.UpdateCustomer.StoreCustomerDto, Models.UpdateCustomer.StoreCustomer>();
        }
    }
}