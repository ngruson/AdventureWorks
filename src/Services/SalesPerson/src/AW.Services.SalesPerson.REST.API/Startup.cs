using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.Services.SalesPerson.Infrastructure.EFCore;
using AW.Services.SalesPerson.REST.API.Extensions;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Api;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AW.Services.SalesPerson.REST.API
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

            services.AddControllers();
            services.AddMvcCore()
                .AddApiExplorer();

            services.AddApiVersioning(options => options.ReportApiVersions = true)
                .AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    }
                );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("AuthN:Authority");
                    options.Audience = "salesperson-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
            services.AddSwaggerDocumentation("Sales Person API");

            services.AddTransient(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(Configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    builder.Options,
                    typeof(EfRepository<>).Assembly,
                    provider.GetService<IMediator>()
                );
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(GetSalesPersonsQuery).Assembly);
            services.AddMediatR(typeof(GetSalesPersonsQuery));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var virtualPath = "/salesperson-api";
            app.Map(virtualPath, builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                }

                builder.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Sales Person API");
                builder.UseRouting();
                builder.UseAuthentication();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}