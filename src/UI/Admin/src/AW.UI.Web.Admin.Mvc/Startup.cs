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
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AW.UI.Web.Admin.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var oidcConfig = new OpenIdConnectConfigurationBuilder(Configuration).Build();

            services
                .AddCustomMvc()
                .AddHttpClients(Configuration, oidcConfig)
                .AddCustomIntegrations()
                .AddCustomAuthentication(Configuration, oidcConfig)
                .AddCustomHealthCheck(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var virtualPath = "/ui-web-admin-mvc";

            app.Map(virtualPath, builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                    IdentityModelEventSource.ShowPII = true;
                }
                else
                {
                    builder.UseExceptionHandler("/Error");
                }

                builder.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                builder.Use(async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    await next();
                });

                builder.UseStaticFiles();
                builder.UseCookiePolicy();
                builder.UseRouting();
                builder.UseAuthentication();
                builder.UseAuthorization();

                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Customer}/{action=Index}/{id?}");

                    endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                    {
                        Predicate = r => r.Name.Contains("self")
                    });
                });
            });
        }
    }

    static class CustomExtensionsMethods
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(GetSalesPersonsQuery));

            services.AddScoped<ICustomerService, CustomerService>();
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

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, OpenIdConnectConfiguration oidcConfig)
        {
            if (oidcConfig.IdentityProvider == IdentityProvider.IdentityServer)
                services.AddOpenIdConnectAccessTokenManagement();

            services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:CustomerApiRead"] }
            );

            services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:ReferenceDataApiRead"] }
            );

            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesOrderAPI:Uri"]);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:SalesOrderApiRead"] }
            );

            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesPersonAPI:Uri"]);
            })
            .AddUserAccessTokenHandler(
                oidcConfig.IdentityProvider,
                new[] { configuration["AuthN:ApiScopes:SalesPersonApiRead"] }
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

                    options.Scope.Add(configuration["AuthN:ApiScopes:CustomerApiRead"]);
                    options.Scope.Add(configuration["AuthN:ApiScopes:ReferenceDataApiRead"]);
                });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]));
            hcBuilder.AddUrlGroup(new Uri(configuration["CustomerAPI:Uri"]), name: "customer-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ReferenceDataAPI:Uri"]), name: "referencedata-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesOrderAPI:Uri"]), name: "salesorder-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesPersonAPI:Uri"]), name: "salesperson-api");

            return services;
        }
    }
}