using AutoMapper;
using AW.Services.Customer.Core.Handlers.CreateCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(Handlers.GetCustomers.GetCustomersQuery).Assembly);

            CreateMap<Entities.Customer, Handlers.GetCustomers.Customer>()
                .Include<Entities.IndividualCustomer, Handlers.GetCustomers.IndividualCustomer>()
                .Include<Entities.StoreCustomer, Handlers.GetCustomers.StoreCustomer>();

            CreateMap<Entities.Customer, Handlers.GetCustomer.Customer>()
                .Include<Entities.IndividualCustomer, Handlers.GetCustomer.IndividualCustomer>()
                .Include<Entities.StoreCustomer, Handlers.GetCustomer.StoreCustomer>();

            CreateMap<Handlers.CreateCustomer.Customer, Entities.Customer>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.Addresses, opt => opt.MapFrom((src, dest, m, ctx) =>
                        MapAddresses(src.Addresses, dest, ctx)
                    )
                )
                .Include<IndividualCustomer, Entities.IndividualCustomer>()
                .Include<StoreCustomer, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Entities.Customer, CreatedCustomer>()
                .Include<Entities.StoreCustomer, CreatedStoreCustomer>()
                .Include<Entities.IndividualCustomer, CreatedIndividualCustomer>();

            CreateMap<Handlers.UpdateCustomer.Customer, Entities.Customer>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .Include<Handlers.UpdateCustomer.IndividualCustomer, Entities.IndividualCustomer>()
                .Include<Handlers.UpdateCustomer.StoreCustomer, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }

        private static IEnumerable<Entities.CustomerAddress> MapAddresses(List<CustomerAddress> addresses, Entities.Customer customer, ResolutionContext ctx)
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
