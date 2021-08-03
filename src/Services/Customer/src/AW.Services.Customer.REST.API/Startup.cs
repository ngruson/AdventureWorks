using AW.Services.Customer.Infrastructure.EFCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.JsonConverters;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using AW.SharedKernel.Api;

namespace AW.Services.Customer.REST.API
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                    options.JsonSerializerOptions.Converters.Add(
                        new CustomerConverter<
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
                    );

                    options.JsonSerializerOptions.Converters.Add(
                        new CustomerConverter<
                            Models.GetCustomer.Customer,
                            Models.GetCustomer.StoreCustomer,
                            Models.GetCustomer.IndividualCustomer>()
                    );

                    options.JsonSerializerOptions.Converters.Add(
                        new CustomerConverter<
                            Models.UpdateCustomer.Customer,
                            Models.UpdateCustomer.StoreCustomer,
                            Models.UpdateCustomer.IndividualCustomer>()
                    );
                });

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
                    options.Audience = "customer-api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
            services.AddSwaggerDocumentation("Customer API");

            services.AddDbContext<AWContext>(c =>
            {
                c.LogTo(Console.WriteLine);
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
                c.EnableSensitiveDataLogging();
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(c => c.AddCollectionMappers(), typeof(MappingProfile).Assembly, typeof(GetCustomersQuery).Assembly);
            services.AddMediatR(typeof(GetCustomersQuery));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            var virtualPath = "/customer-api";

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

                builder.UseSwaggerDocumentation(virtualPath, Configuration, provider, "Customer API");
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