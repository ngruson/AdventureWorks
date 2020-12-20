using AutoMapper;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Infrastructure.Api.WCF.AddressTypeService;
using AW.Infrastructure.Api.WCF.ContactTypeService;
using AW.Infrastructure.Api.WCF.CountryService;
using AW.Infrastructure.Api.WCF.CustomerService;
using AW.Infrastructure.Api.WCF.SalesOrderService;
using AW.Infrastructure.Api.WCF.SalesPersonService;
using AW.Infrastructure.Api.WCF.SalesTerritoryService;
using AW.Infrastructure.Api.WCF.StateProvinceService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel;

namespace AW.Infrastructure.Api.WCF
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICustomerApi, CustomerServiceAdapter>();

            services.AddWCFServices(configuration);
        }

        private static void AddWCFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAddressTypeService>(provider =>
            {
                var client = new AddressTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["AddressTypeService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<IContactTypeService>(provider =>
            {
                var client = new ContactTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["ContactTypeService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ICountryService>(provider =>
            {
                var client = new CountryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["CountryService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ICustomerService>(provider =>
            {
                var client = new CustomerServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["CustomerService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesOrderService>(provider =>
            {
                var client = new SalesOrderServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesOrderService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesPersonService>(provider =>
            {
                var client = new SalesPersonServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesPersonService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesTerritoryService>(provider =>
            {
                var client = new SalesTerritoryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesTerritoryService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<IStateProvinceService>(provider =>
            {
                var client = new StateProvinceServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["StateProvinceService:EndpointAddress"])
                );

                return client;
            });
        }
    }
}