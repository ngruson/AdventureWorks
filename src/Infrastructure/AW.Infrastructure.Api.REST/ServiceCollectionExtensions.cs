using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.ProductApi;
using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
using AW.Core.Abstractions.Api.StateProvinceApi;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AW.Infrastructure.Api.REST
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAddressTypeApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAddressTypeApi>(provider =>
            {
                var api = new AddressTypeApi(
                    provider.GetService<ILogger<AddressTypeApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["AddressTypeApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["AddressTypeApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureContactTypeApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactTypeApi>(provider =>
            {
                var api = new ContactTypeApi(
                    provider.GetService<ILogger<ContactTypeApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["ContactTypeApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["ContactTypeApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureCountryApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICountryApi>(provider =>
            {
                var api = new CountryApi(
                    provider.GetService<ILogger<CountryApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["CountryApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["CountryApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureCustomerApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerApi>(provider =>
            {
                var api = new CustomerApi(
                    provider.GetService<ILogger<CustomerApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["CustomerApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["CustomerApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureProductApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductApi>(provider =>
            {
                var api = new ProductApi(
                    provider.GetService<ILogger<ProductApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["ProductApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["ProductApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureSalesPersonApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISalesPersonApi>(provider =>
            {
                var api = new SalesPersonApi(
                    provider.GetService<ILogger<SalesPersonApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["SalesPersonApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["SalesPersonApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureSalesTerritoryApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISalesTerritoryApi>(provider =>
            {
                var api = new SalesTerritoryApi(
                    provider.GetService<ILogger<SalesTerritoryApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["SalesTerritoryApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["SalesTerritoryApi:SubscriptionKey"]);

                return api;
            });
        }

        public static void ConfigureStateProvinceApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStateProvinceApi>(provider =>
            {
                var api = new StateProvinceApi(
                    provider.GetService<ILogger<StateProvinceApi>>(),
                    provider.GetService<IHttpRequestFactory>(),
                    configuration["StateProvinceApi:Uri"]
                );

                api.Headers.Add("Ocp-Apim-Subscription-Key", configuration["StateProvinceApi:SubscriptionKey"]);

                return api;
            });
        }
    }
}