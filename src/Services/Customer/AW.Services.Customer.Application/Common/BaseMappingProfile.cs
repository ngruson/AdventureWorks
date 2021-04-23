using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AW.Services.Customer.Application.Common
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Domain.Customer, GetCustomers.CustomerDto>()
                .Include<Domain.IndividualCustomer, GetCustomers.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, GetCustomers.StoreCustomerDto>();

            CreateMap<Domain.Customer, GetCustomer.CustomerDto>()
                .Include<Domain.IndividualCustomer, GetCustomer.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, GetCustomer.StoreCustomerDto>();

            CreateMap<Domain.Customer, AddCustomer.CustomerDto>()
                .Include<Domain.IndividualCustomer, AddCustomer.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, AddCustomer.StoreCustomerDto>();

            CreateMap<Domain.Customer, UpdateCustomer.CustomerDto>()
                .Include<Domain.IndividualCustomer, UpdateCustomer.IndividualCustomerDto>()
                .Include<Domain.StoreCustomer, UpdateCustomer.StoreCustomerDto>()
                .ReverseMap()
                .Include<UpdateCustomer.IndividualCustomerDto, Domain.IndividualCustomer>()
                .Include<UpdateCustomer.StoreCustomerDto, Domain.StoreCustomer>();
        }

        protected void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(x => !x.IsAbstract);
            var interfaceType = typeof(IMapFrom<>);
            var methodName = nameof(IMapFrom<object>.Mapping);
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                    .ToList();

                // Has the type implemented any IMapFrom<T>?
                if (interfaces.Count > 0)
                {

                    // Yes, then let's create an instance
                    var instance = Activator.CreateInstance(type);

                    // and invoke each implementation of `.Mapping()`
                    foreach (var i in interfaces)
                    {
                        var methodInfo = i.GetMethod(methodName, argumentTypes);

                        methodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}