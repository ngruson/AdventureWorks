using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.Customer.WCF
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Core.Handlers.GetCustomers.CustomerDto, Messages.ListCustomers.Customer>()

                .Include<Core.Handlers.GetCustomers.IndividualCustomerDto, Messages.ListCustomers.IndividualCustomer>()
                .Include<Core.Handlers.GetCustomers.StoreCustomerDto, Messages.ListCustomers.Store>();

            CreateMap<Core.Handlers.GetCustomer.CustomerDto, Messages.GetCustomer.Customer>()
                .Include<Core.Handlers.GetCustomer.IndividualCustomerDto, Messages.GetCustomer.IndividualCustomer>()
                .Include<Core.Handlers.GetCustomer.StoreCustomerDto, Messages.GetCustomer.StoreCustomer>();

            CreateMap<Messages.UpdateCustomer.Customer, Core.Handlers.UpdateCustomer.CustomerDto>()                
                .Include<Messages.UpdateCustomer.IndividualCustomer, Core.Handlers.UpdateCustomer.IndividualCustomerDto>()
                .Include<Messages.UpdateCustomer.StoreCustomer, Core.Handlers.UpdateCustomer.StoreCustomerDto>()
                .ReverseMap();
        }
    }
}