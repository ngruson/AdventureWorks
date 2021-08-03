using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.Infrastructure.EFCore;
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("AuthN:Authority");
                    options.Audience = "referencedata-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
            services.AddSwaggerDocumentation("Reference Data API");

            services.AddDbContext<AWContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection"))
            );
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
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

                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Reference Data API");
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