using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

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
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISalesOrderViewModelService, SalesOrderViewModelService>();
            services.AddScoped<ISalesPersonViewModelService, SalesPersonViewModelService>();
            services.AddScoped<ISalesTerritoryViewModelService, SalesTerritoryViewModelService>();
            services.AddScoped<IReferenceDataService, ReferenceDataService>();

            services.AddAccessTokenManagement();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["CustomerAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ReferenceDataAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["SalesOrderAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["SalesPersonAPI:Uri"]);
            })
            .AddUserAccessTokenHandler();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Configuration["AuthN:Authority"];
                    options.ClientId = Configuration["AuthN:ClientId"];
                    options.ClientSecret = Configuration["AuthN:ClientSecret"];
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
            });
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
                });
            });
        }
    }
}