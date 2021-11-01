using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddControllersWithViews()
                .Services
                .AddAutoMapper(typeof(Startup))
                .AddApplicationServices()
                .AddHttpClientServices(Configuration)
                .AddCustomAuthentication(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var virtualPath = "/mvc-store";

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
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            });
        }
    }

    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAccessTokenManagement();

            services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]);
            })
            .AddClientAccessTokenHandler();

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
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("offline_access");
                options.Scope.Add("basket-api.read");
                options.Scope.Add("basket-api.write");
                options.Scope.Add("product-api.read");
            });

            return services;
        }
    }
}