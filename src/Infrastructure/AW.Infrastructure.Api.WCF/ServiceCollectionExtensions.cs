using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.ProductApi;
using AW.Infrastructure.Api.WCF.AddressTypeService;
using AW.Infrastructure.Api.WCF.AutoMapper;
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
            services.AddAutoMapper(typeof(CustomerProfile));            
            services.AddWCFServices(configuration);
        }

        private static void AddWCFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAddressTypeService>(provider =>
            {
                var client = new AddressTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["AddressTypeAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<IContactTypeService>(provider =>
            {
                var client = new ContactTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["ContactTypeAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<ICountryService>(provider =>
            {
                var client = new CountryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["CountryAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<ICustomerService>(provider =>
            {
                var client = new CustomerServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["CustomerAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<ISalesOrderService>(provider =>
            {
                var client = new SalesOrderServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesOrderAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<ISalesPersonService>(provider =>
            {
                var client = new SalesPersonServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesPersonAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<ISalesTerritoryService>(provider =>
            {
                var client = new SalesTerritoryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["SalesTerritoryAPI:Uri"])
                );

                return client;
            });
            services.AddScoped<IStateProvinceService>(provider =>
            {
                var client = new StateProvinceServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(configuration["StateProvinceAPI:Uri"])
                );

                return client;
            });
        }
    }
}