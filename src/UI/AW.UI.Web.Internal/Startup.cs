using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.ProductApi;
using AW.Core.Abstractions.Api.SalesOrderApi;
using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
using AW.Core.Abstractions.Api.StateProvinceApi;
using AW.Infrastructure.Api.REST;
using AW.Infrastructure.Api.WCF;
using AW.Infrastructure.Http;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            services.AddScoped<ICustomerViewModelService, CustomerViewModelService>();
            services.AddScoped<ISalesOrderViewModelService, SalesOrderViewModelService>();
            services.AddScoped<ISalesPersonViewModelService, SalesPersonViewModelService>();
            services.AddScoped<ISalesTerritoryViewModelService, SalesTerritoryViewModelService>();

            services.AddScoped<IHttpRequestFactory, HttpRequestFactory>();
            services.AddScoped<IHttpRequestBuilder, HttpRequestBuilder>();
            services.ConfigureServices(Configuration);
            services.ConfigureAddressTypeApi(Configuration);
            services.ConfigureContactTypeApi(Configuration);
            services.ConfigureCountryApi(Configuration);
            services.ConfigureCustomerApi(Configuration);
            services.AddScoped<IProductApi, ProductServiceWCF>();
            services.AddScoped<ISalesOrderApi, SalesOrderServiceWCF>();
            services.ConfigureSalesPersonApi(Configuration);
            services.ConfigureSalesTerritoryApi(Configuration);
            services.ConfigureStateProvinceApi(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}