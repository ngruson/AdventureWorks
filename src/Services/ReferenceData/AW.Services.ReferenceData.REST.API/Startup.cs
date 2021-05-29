using Ardalis.Specification;
using AW.Services.Product.REST.API.Extensions;
using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.Persistence.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AW.Services.ReferenceData.REST.API
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

            services.AddSwaggerGen();

            services.AddDbContext<AWContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection"))
            );
            services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepository<>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(typeof(GetAddressTypesQuery));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var virtualPath = "/reference-data-api";
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

                builder.UseSwaggerDocumentation(virtualPath, provider);
                builder.UseRouting();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reference Data API v1"));
            ////.UseSwaggerUI(options =>
            ////{
            ////    foreach (var description in provider.ApiVersionDescriptions)
            ////    {
            ////        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Reference Data API {description.GroupName.ToUpperInvariant()}");
            ////        options.RoutePrefix = string.Empty;
            ////    }
            ////    options.DocumentTitle = "Reference Data API Documentation";
            ////});

            //app.UseRouting();
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}