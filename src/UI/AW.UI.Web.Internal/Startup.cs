using AW.UI.Web.Common.ApiClients.CustomerApi;
using AW.UI.Web.Common.ApiClients.ReferenceDataApi;
using AW.UI.Web.Common.ApiClients.SalesOrderApi;
using AW.UI.Web.Common.ApiClients.SalesPersonApi;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISalesOrderViewModelService, SalesOrderViewModelService>();
            services.AddScoped<ISalesPersonViewModelService, SalesPersonViewModelService>();
            services.AddScoped<ISalesTerritoryViewModelService, SalesTerritoryViewModelService>();

            services.AddScoped<IReferenceDataService, ReferenceDataService>();

            services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["CustomerAPI:Uri"]);
            });
            services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ReferenceDataAPI:Uri"]);
            });
            services.AddHttpClient<ISalesOrderApiClient, SalesOrderApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["SalesOrderAPI:Uri"]);
            });
            services.AddHttpClient<ISalesPersonApiClient, SalesPersonApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["SalesPersonAPI:Uri"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

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