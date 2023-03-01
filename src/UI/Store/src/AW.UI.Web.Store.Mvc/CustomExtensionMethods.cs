using AW.SharedKernel.Interfaces;
using AW.SharedKernel.OpenIdConnect;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using AW.UI.Web.Store.Mvc.Services;
using AW.UI.Web.Store.Mvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using AW.UI.Web.SharedKernel.Product.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Caching;
using AW.UI.Web.SharedKernel.SalesPerson.Caching;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Store.Mvc
{
    static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddScoped<IApplication, Application>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();
            services.AddMediatR(typeof(GetSalesPersonsQuery));

            services.AddScoped<ICache<AddressType>, AddressTypeCache>();
            services.AddScoped<ICache<ContactType>, ContactTypeCache>();
            services.AddScoped<ICache<CountryRegion>, CountryCache>();
            services.AddScoped<ICache<ProductCategory>, ProductCategoryCache>();
            services.AddScoped<ICache<SalesPerson>, SalesPersonCache>();
            services.AddScoped<ICache<ShipMethod>, ShipMethodCache>();
            services.AddScoped<ICache<StateProvince>, StatesProvinceCache>();
            services.AddScoped<ICache<Territory>, TerritoryCache>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, OpenIdConnectConfiguration oidcConfig)
        {
            if (oidcConfig.IdentityProvider == IdentityProvider.IdentityServer)
                services.AddOpenIdConnectAccessTokenManagement();

            services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:BasketApiRead"]! }
            );

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:CustomerApiRead"]! }
            );

            services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:ProductApiRead"]! }
            );

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:ReferenceDataApiRead"]! }
            );

            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesOrderAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:SalesOrderApiRead"]! }
            );

            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesPersonAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:SalesPersonApiRead"]! }
            );

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            if (configuration["AuthN:IdP"] == "AzureAd")
            {
                services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApp(options => configuration.Bind("AuthN:AzureAd", options))
                        .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddInMemoryTokenCaches();
            }
            else if (configuration["AuthN:IdP"] == "IdSrv")
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("cookie")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthN:Authority"];
                    options.ClientId = configuration["AuthN:ClientId"];
                    options.ClientSecret = configuration["AuthN:ClientSecret"];
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.SaveTokens = true;
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("basket-api.read");
                    options.Scope.Add("basket-api.write");
                    options.Scope.Add("customer-api.read");
                    options.Scope.Add("product-api.read");
                    options.Scope.Add("referencedata-api.read");
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClaimActions.MapUniqueJsonKey("customer_number", "customer_number");
                });
            }

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);
            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]!));
            hcBuilder.AddUrlGroup(new Uri(configuration["BasketAPI:Uri"]!), name: "basket-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ProductAPI:Uri"]!), name: "product-api");

            return services;
        }
    }
}
