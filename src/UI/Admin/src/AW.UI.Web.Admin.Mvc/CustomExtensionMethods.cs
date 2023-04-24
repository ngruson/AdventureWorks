using AW.SharedKernel.Caching;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.OpenIdConnect;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Caching;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.ReferenceData.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.SharedKernel.SalesPerson.Caching;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.IdentityModel.Tokens.Jwt;

namespace AW.UI.Web.Admin.Mvc
{
    static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetSalesPersonsQuery).Assembly)
            );

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductModelService, ProductModelService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<ISalesPersonViewModelService, SalesPersonViewModelService>();

            services.AddScoped<ICache<AddressType>, AddressTypeCache>();
            services.AddScoped<ICache<ContactType>, ContactTypeCache>();
            services.AddScoped<ICache<CountryRegion>, CountryCache>();
            services.AddScoped<ICache<ProductCategory>, ProductCategoryCache>();
            services.AddScoped<ICache<SalesPerson>, SalesPersonCache>();
            services.AddScoped<ICache<ShipMethod>, ShipMethodCache>();
            services.AddScoped<ICache<StateProvince>, StatesProvinceCache>();
            services.AddScoped<ICache<Territory>, TerritoryCache>();

            services.AddScoped<CustomerConverter<SharedKernel.Customer.Handlers.GetCustomers.Customer,
                SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer,
                SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer>>();
            services.AddScoped<CustomerConverter<SharedKernel.Customer.Handlers.GetCustomer.Customer,
                SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer,
                SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer>>();
            services.AddScoped<CustomerConverter<SharedKernel.Customer.Handlers.UpdateCustomer.Customer,
                SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer,
                SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>>();

            services.AddScoped<CustomerConverter<SharedKernel.SalesOrder.Handlers.GetSalesOrders.Customer,
                SharedKernel.SalesOrder.Handlers.GetSalesOrders.StoreCustomer,
                SharedKernel.SalesOrder.Handlers.GetSalesOrders.IndividualCustomer>>();
            services.AddScoped<CustomerConverter<SharedKernel.SalesOrder.Handlers.GetSalesOrder.Customer,
                SharedKernel.SalesOrder.Handlers.GetSalesOrder.StoreCustomer,
                SharedKernel.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer>>();
            services.AddScoped<CustomerConverter<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.Customer,
                SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.StoreCustomer,
                SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.IndividualCustomer>>();

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
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:CustomerApiRead"]! }
            );

            services.AddHttpClient<IDepartmentApiClient, DepartmentApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["DepartmentAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:DepartmentApiRead"]! }
            );

            services.AddHttpClient<IEmployeeApiClient, EmployeeApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["EmployeeAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:EmployeeApiRead"]! }
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

            services.AddHttpClient<IShiftApiClient, ShiftApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ShiftAPI:Uri"]!);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:ShiftApiRead"]! }
            );

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration, OpenIdConnectConfiguration oidcConfig)
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

                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = oidcConfig.Authority;
                    options.ClientId = oidcConfig.ClientId;
                    options.ClientSecret = oidcConfig.ClientSecret;
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.Scope.Clear();

                    var scopes = oidcConfig.Scopes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var scope in scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.Scope.Add(configuration["AuthN:ApiScopes:CustomerApiRead"]!);
                    options.Scope.Add(configuration["AuthN:ApiScopes:ReferenceDataApiRead"]!);
                });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);
            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));
            hcBuilder.AddUrlGroup(new Uri(configuration["CustomerAPI:Uri"]!), name: "customer-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ReferenceDataAPI:Uri"]!), name: "referencedata-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesOrderAPI:Uri"]!), name: "salesorder-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesPersonAPI:Uri"]!), name: "salesperson-api");

            return services;
        }
    }
}
