using AW.SharedKernel.Caching;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.SharedKernel.Interfaces.Api;
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace AW.UI.Web.Internal
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
            services
                .AddCustomMvc()
                .AddHttpClients(Configuration)
                .AddCustomIntegrations()
                .AddCustomAuthentication(Configuration)
                .AddCustomHealthCheck(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var virtualPath = "/mvc-internal";

            app.Map(virtualPath, builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
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
            services.AddControllersWithViews();
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
            services.AddScoped<ICache<SalesPerson>, SalesPersonCache>();
            services.AddScoped<ICache<ShipMethod>, ShipMethodCache>();
            services.AddScoped<ICache<StateProvince>, StateProvinceCache>();
            services.AddScoped<ICache<Territory>, TerritoryCache>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAccessTokenManagement();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesOrderAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SalesPersonAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthN:Authority"];
                    options.ClientId = configuration["AuthN:ClientId"];
                    options.ClientSecret = configuration["AuthN:ClientSecret"];
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("customer-api.read");
                    options.Scope.Add("salesorder-api.read");
                    options.Scope.Add("salesperson-api.read");
                    options.Scope.Add("referencedata-api.read");
                });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]));
            hcBuilder.AddUrlGroup(new Uri(configuration["CustomerAPI:Uri"]), name: "customer-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ReferenceDataAPI:Uri"]), name: "referencedata-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesOrderAPI:Uri"]), name: "salesorder-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["SalesPersonAPI:Uri"]), name: "salesperson-api");

            return services;
        }
    }
}