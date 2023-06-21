using AutoMapper.EquivalencyExpression;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Infrastructure.EFCore.Configurations;
using AW.Services.Infrastructure.Filters;
using AW.Services.SharedKernel.JsonConverters;
using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Api;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.OpenIdConnect;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using AW.Services.Customer.Core.Handlers.CreateCustomer;
using AW.Services.Customer.Core.Handlers.CreateCustomerAddress;
using AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone;
using AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;

namespace AW.Services.Customer.REST.API
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
                options.AddResultConvention(resultStatusMap => resultStatusMap
                    .AddDefaultMap()
                );
            });

            services.AddTransient<CustomerConverter<Core.Handlers.GetCustomers.Customer,
                Core.Handlers.GetCustomers.StoreCustomer,
                Core.Handlers.GetCustomers.IndividualCustomer>>();
            services.AddTransient<CustomerConverter<Core.Handlers.GetCustomer.Customer,
                Core.Handlers.GetCustomer.StoreCustomer,
                Core.Handlers.GetCustomer.IndividualCustomer>>();
            services.AddTransient<CustomerConverter<Core.Handlers.UpdateCustomer.Customer,
                Core.Handlers.UpdateCustomer.StoreCustomer,
                Core.Handlers.UpdateCustomer.IndividualCustomer>>();
            services.AddTransient<EmailAddressConverter>();

            services.AddOptions<ConfigureJsonOptions>();
            services.AddSingleton<IConfigureOptions<JsonOptions>, ConfigureJsonOptions>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
            services.AddScoped<IValidator<CreateCustomerAddressCommand>, CreateCustomerAddressCommandValidator>();
            services.AddScoped<IValidator<CreateIndividualCustomerEmailAddressCommand>, CreateIndividualCustomerEmailAddressCommandValidator>();
            services.AddScoped<IValidator<CreateIndividualCustomerPhoneCommand>, CreateIndividualCustomerPhoneCommandValidator>();
            services.AddScoped<IValidator<CreateStoreCustomerContactCommand>, CreateStoreCustomerContactCommandValidator>();
            services.AddScoped<IValidator<DeleteCustomerCommand>, DeleteCustomerCommandValidator>();
            services.AddScoped<IValidator<DeleteCustomerAddressCommand>, DeleteCustomerAddressCommandValidator>();
            services.AddScoped<IValidator<DeleteIndividualCustomerEmailAddressCommand>, DeleteIndividualCustomerEmailAddressCommandValidator>();
            services.AddScoped<IValidator<DeleteIndividualCustomerPhoneCommand>, DeleteIndividualCustomerPhoneCommandValidator>();
            services.AddScoped<IValidator<DeleteStoreCustomerContactCommand>, DeleteStoreCustomerContactCommandValidator>();
            services.AddScoped<IValidator<GetPreferredAddressQuery>, GetPreferredAddressQueryValidator>();
            services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
            services.AddScoped<IValidator<UpdateCustomerAddressCommand>, UpdateCustomerAddressCommandValidator>();
            services.AddScoped<IValidator<UpdateStoreCustomerContactCommand>, UpdateStoreCustomerContactCommandValidator>();

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:58093/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
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

            return services;
        }

        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "admin-cache";
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            if (configuration["AuthN:IdP"] == "AzureAd")
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(configuration.GetSection("AuthN:AzureAd"));

                services.AddAuthorizationBuilder()
                    .AddPolicy("customer-read", policy =>
                        policy
                            .RequireScope("customer-api.read")
                    )
                    .AddPolicy("customer-write", policy =>
                        policy
                            .RequireScope("customer-api.write")
                    );
            }
            else if (configuration["AuthN:IdP"] == "IdSrv")
            {
                var oidcConfig = new OpenIdConnectConfigurationBuilder(configuration).Build();

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = oidcConfig?.Authority;
                        options.Audience = "customer-api";
                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    });
            }

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocumentation("Customer API");

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(provider =>
            {
                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

                return new AWContext(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    builder.Options,
                    provider.GetRequiredService<IMediator>(),
                    typeof(CustomerConfiguration).Assembly
                );
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddAutoMapper(c => c.AddCollectionMappers(), typeof(MappingProfile).Assembly, typeof(GetCustomersQuery).Assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(
                typeof(GetCustomersQuery).Assembly)
            );

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);

            if (configuration["AuthN:IdP"] == "IdSrv")
                hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:IdSrv:Authority"]!));

            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection")!);

            return services;
        }
    }
}
