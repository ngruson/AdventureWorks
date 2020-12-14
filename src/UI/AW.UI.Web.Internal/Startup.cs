using System.ServiceModel;
using AutoMapper;
using AW.UI.Web.Internal.AddressTypeService;
using AW.UI.Web.Internal.ContactTypeService;
using AW.UI.Web.Internal.CountryService;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.SalesOrderService;
using AW.UI.Web.Internal.SalesPersonService;
using AW.UI.Web.Internal.SalesTerritoryService;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.StateProvinceService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            ConfigureWebservices(services);
        }

        private void ConfigureWebservices(IServiceCollection services)
        {
            services.AddScoped<IAddressTypeService>(provider =>
            {
                var client = new AddressTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["AddressTypeService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<IContactTypeService>(provider =>
            {
                var client = new ContactTypeServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["ContactTypeService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ICountryService>(provider =>
            {
                var client = new CountryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["CountryService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ICustomerService>(provider =>
            {
                var client = new CustomerServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["CustomerService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesOrderService>(provider =>
            {
                var client = new SalesOrderServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["SalesOrderService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesPersonService>(provider =>
            {
                var client = new SalesPersonServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["SalesPersonService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<ISalesTerritoryService>(provider =>
            {
                var client = new SalesTerritoryServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["SalesTerritoryService:EndpointAddress"])
                );

                return client;
            });
            services.AddScoped<IStateProvinceService>(provider =>
            {
                var client = new StateProvinceServiceClient(
                    new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    new EndpointAddress(Configuration["StateProvinceService:EndpointAddress"])
                );

                return client;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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