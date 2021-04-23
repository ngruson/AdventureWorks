using AW.Services.Customer.Application.Common;
using System.Reflection;

namespace AW.Services.Customer.WCF
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Application.GetCustomers.CustomerDto, Messages.ListCustomers.Customer>()

                .Include<Application.GetCustomers.IndividualCustomerDto, Messages.ListCustomers.IndividualCustomer>()
                .Include<Application.GetCustomers.StoreCustomerDto, Messages.ListCustomers.Store>();

            CreateMap<Application.GetCustomer.CustomerDto, Messages.GetCustomer.Customer>()
                .Include<Application.GetCustomer.IndividualCustomerDto, Messages.GetCustomer.IndividualCustomer>()
                .Include<Application.GetCustomer.StoreCustomerDto, Messages.GetCustomer.StoreCustomer>();

            CreateMap<Messages.UpdateCustomer.Customer, Application.UpdateCustomer.CustomerDto>()                
                .Include<Messages.UpdateCustomer.IndividualCustomer, Application.UpdateCustomer.IndividualCustomerDto>()
                .Include<Messages.UpdateCustomer.StoreCustomer, Application.UpdateCustomer.StoreCustomerDto>()
                .ReverseMap();
        }
    }
}