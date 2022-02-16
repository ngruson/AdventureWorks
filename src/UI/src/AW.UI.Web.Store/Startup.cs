using AW.SharedKernel.Interfaces;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
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

namespace AW.UI.Web.Store
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
                .AddCustomIntegrations()
                .AddHttpClients(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddCustomHealthCheck(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var virtualPath = "/mvc-store";

            app.Map(virtualPath, builder =>
            {
                builder.UseExceptionHandler("/Error");

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
                        pattern: "{controller=Home}/{action=Index}/{id?}");

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
            services.AddScoped<IApplication, Application>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReferenceDataService, ReferenceDataService>();
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAccessTokenManagement();

            services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]);
            })
            .AddClientAccessTokenHandler();

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

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

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);
            hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]));
            hcBuilder.AddUrlGroup(new Uri(configuration["BasketAPI:Uri"]), name: "basket-api");
            hcBuilder.AddUrlGroup(new Uri(configuration["ProductAPI:Uri"]), name: "product-api");

            return services;
        }
    }
}