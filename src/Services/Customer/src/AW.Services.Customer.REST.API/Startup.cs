using Ardalis.Specification;
using AW.Services.Customer.REST.API.Extensions;
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
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.JsonConverters;
using AW.Services.Customer.Core.Handlers.GetCustomers;

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

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer API", Version = "v1" });
            });

            services.AddDbContext<AWContext>(c =>
            {
                c.LogTo(Console.WriteLine);
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
                c.EnableSensitiveDataLogging();
            });
            services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepository<>));
            services.AddAutoMapper(c => c.AddCollectionMappers(), typeof(MappingProfile).Assembly, typeof(GetCustomersQuery).Assembly);
            services.AddMediatR(typeof(GetCustomersQuery));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
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

                builder.UseSwaggerDocumentation(virtualPath, provider);
                builder.UseRouting();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}