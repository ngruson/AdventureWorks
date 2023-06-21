using AW.SharedKernel.Interfaces;
using AW.SharedKernel.OpenIdConnect;
using AW.UI.Web.Store.Mvc.Services;
using AW.UI.Web.Store.Mvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IdentityModel.Tokens.Jwt;
using AW.SharedKernel.Caching;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using AW.UI.Web.Infrastructure.Api.ApiClients;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Caching;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Caching;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons;
using AW.UI.Web.Infrastructure.Api.Product.Caching;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.SharedKernel.JsonConverters;

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
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetSalesPersonsQuery).Assembly)
            );

            services.AddScoped<ICache<AddressType>, AddressTypeCache>();
            services.AddScoped<ICache<ContactType>, ContactTypeCache>();
            services.AddScoped<ICache<CountryRegion>, CountryCache>();
            services.AddScoped<ICache<ProductCategory>, ProductCategoryCache>();
            services.AddScoped<ICache<SalesPerson>, SalesPersonCache>();
            services.AddScoped<ICache<ShipMethod>, ShipMethodCache>();
            services.AddScoped<ICache<StateProvince>, StatesProvinceCache>();
            services.AddScoped<ICache<Territory>, TerritoryCache>();

            services.AddScoped<CustomerConverter<Infrastructure.Api.Customer.Handlers.GetCustomers.Customer,
                Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomer,
                Infrastructure.Api.Customer.Handlers.GetCustomers.IndividualCustomer>>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, OpenIdConnectConfiguration oidcConfig)
        {
            string basketApiReadScope;
            string customerApiReadScope;
            string departmentApiReadScope;
            string employeeApiReadScope;
            string productApiReadScope;
            string referenceDataApiReadScope;
            string salesOrderApiReadScope;
            string salesPersonApiReadScope;
            string shiftApiReadScope;

            if (oidcConfig.IdentityProvider == IdentityProvider.IdentityServer)
            {
                services.AddOpenIdConnectAccessTokenManagement();

                basketApiReadScope = configuration["AuthN:IdSrv:ApiScopes:BasketApiRead"]!;
                customerApiReadScope = configuration["AuthN:IdSrv:ApiScopes:CustomerApiRead"]!;
                departmentApiReadScope = configuration["AuthN:IdSrv:ApiScopes:DepartmentApiRead"]!;
                employeeApiReadScope = configuration["AuthN:IdSrv:ApiScopes:EmployeeApiRead"]!;
                productApiReadScope = configuration["AuthN:IdSrv:ApiScopes:ProductApiRead"]!;
                referenceDataApiReadScope = configuration["AuthN:IdSrv:ApiScopes:ReferenceDataApiRead"]!;
                salesOrderApiReadScope = configuration["AuthN:IdSrv:ApiScopes:SalesOrderApiRead"]!;
                salesPersonApiReadScope = configuration["AuthN:IdSrv:ApiScopes:SalesPersonApiRead"]!;
                shiftApiReadScope = configuration["AuthN:IdSrv:ApiScopes:ShiftApiRead"]!;
            }
            else
            {
                basketApiReadScope = configuration["AuthN:AzureAd:ApiScopes:BasketApiRead"]!;
                customerApiReadScope = configuration["AuthN:AzureAd:ApiScopes:CustomerApiRead"]!;
                departmentApiReadScope = configuration["AuthN:AzureAd:ApiScopes:DepartmentApiRead"]!;
                employeeApiReadScope = configuration["AuthN:AzureAd:ApiScopes:EmployeeApiRead"]!;
                productApiReadScope = configuration["AuthN:AzureAd:ApiScopes:ProductApiRead"]!;
                referenceDataApiReadScope = configuration["AuthN:AzureAd:ApiScopes:ReferenceDataApiRead"]!;
                salesOrderApiReadScope = configuration["AuthN:AzureAd:ApiScopes:SalesOrderApiRead"]!;
                salesPersonApiReadScope = configuration["AuthN:AzureAd:ApiScopes:SalesPersonApiRead"]!;
                shiftApiReadScope = configuration["AuthN:AzureAd:ApiScopes:ShiftApiRead"]!;
            }

            services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { basketApiReadScope }
            );

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { customerApiReadScope }
            );

            services.AddHttpClient<IDepartmentApiClient, DepartmentApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["DepartmentAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { departmentApiReadScope }
            );

            services.AddHttpClient<IEmployeeApiClient, EmployeeApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["EmployeeAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { employeeApiReadScope }
            );

            services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { productApiReadScope }
            );

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { referenceDataApiReadScope }
            );

            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesOrderAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { salesOrderApiReadScope }
            );

            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesPersonAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { salesPersonApiReadScope }
            );

            services.AddHttpClient<IShiftApiClient, ShiftApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ShiftAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { shiftApiReadScope }
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
                    options.Authority = configuration["AuthN:IdSrv:Authority"];
                    options.ClientId = configuration["AuthN:IdSrv:ClientId"];
                    options.ClientSecret = configuration["AuthN:IdSrv:ClientSecret"];
                    options.RequireHttpsMetadata = false;
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
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));
            hcBuilder.AddUrlGroup(new Uri(configuration["BasketAPI:Uri"]!), name: "basket-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ProductAPI:Uri"]!), name: "product-api");

            return services;
        }
    }
}
