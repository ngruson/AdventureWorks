using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(Handlers.GetCustomers.GetCustomersQuery).Assembly);

            CreateMap<Entities.Customer, Handlers.GetAllCustomers.CustomerDto>()
                .Include<Entities.IndividualCustomer, Handlers.GetAllCustomers.IndividualCustomerDto>()
                .Include<Entities.StoreCustomer, Handlers.GetAllCustomers.StoreCustomerDto>();

            CreateMap<Entities.Customer, Handlers.GetCustomer.CustomerDto>()
                .Include<Entities.IndividualCustomer, Handlers.GetCustomer.IndividualCustomerDto>()
                .Include<Entities.StoreCustomer, Handlers.GetCustomer.StoreCustomerDto>();

            CreateMap<Handlers.AddCustomer.CustomerDto, Entities.Customer>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.Addresses, opt => opt.MapFrom((src, dest, m, ctx) =>
                        MapAddresses(src.Addresses, dest, ctx)
                    )
                )
                .Include<Handlers.AddCustomer.IndividualCustomerDto, Entities.IndividualCustomer>()
                .Include<Handlers.AddCustomer.StoreCustomerDto, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Handlers.UpdateCustomer.CustomerDto, Entities.Customer>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .Include<Handlers.UpdateCustomer.IndividualCustomerDto, Entities.IndividualCustomer>()
                .Include<Handlers.UpdateCustomer.StoreCustomerDto, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Handlers.GetCustomer.CustomerDto, Models.GetCustomers.Customer>()
                .Include<Handlers.GetCustomer.IndividualCustomerDto, Models.GetCustomers.IndividualCustomer>()
                .Include<Handlers.GetCustomer.StoreCustomerDto, Models.GetCustomers.StoreCustomer>();

            CreateMap<Handlers.GetCustomer.CustomerDto, Models.GetCustomer.Customer>()
                .Include<Handlers.GetCustomer.IndividualCustomerDto, Models.GetCustomer.IndividualCustomer>()
                .Include<Handlers.GetCustomer.StoreCustomerDto, Models.GetCustomer.StoreCustomer>();

            CreateMap<Models.UpdateCustomer.Customer, Handlers.UpdateCustomer.UpdateCustomerCommand>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src));

            CreateMap<Models.UpdateCustomer.Customer, Handlers.UpdateCustomer.CustomerDto>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .Include<Models.UpdateCustomer.IndividualCustomer, Handlers.UpdateCustomer.IndividualCustomerDto>()
                .Include<Models.UpdateCustomer.StoreCustomer, Handlers.UpdateCustomer.StoreCustomerDto>()
                .ReverseMap()
                .Include<Handlers.UpdateCustomer.IndividualCustomerDto, Models.UpdateCustomer.IndividualCustomer>()
                .Include<Handlers.UpdateCustomer.StoreCustomerDto, Models.UpdateCustomer.StoreCustomer>();
        }

        private static IEnumerable<Entities.CustomerAddress> MapAddresses(List<Handlers.AddCustomer.CustomerAddressDto> addresses, Entities.Customer customer, ResolutionContext ctx)
        {
            addresses.ForEach(_ =>
            {
                var mappedAddress = ctx.Mapper.Map<Entities.CustomerAddress>(_);
                customer.AddAddress(mappedAddress);
            });

            return customer.Addresses;
        }
    }
}