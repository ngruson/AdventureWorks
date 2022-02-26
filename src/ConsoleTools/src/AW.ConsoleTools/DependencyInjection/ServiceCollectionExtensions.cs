using AW.Services.SharedKernel.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AW.Services.Customer.Infrastructure.EFCore.Configurations;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using AW.Services.IdentityServer.Core.Data;
using AW.Services.IdentityServer.Core.Models;
using Microsoft.AspNetCore.Identity;
using AW.Services.Product.Infrastructure.EFCore.Configurations;
using AW.SharedKernel.Interfaces;

namespace AW.ConsoleTools.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddScoped<Func<ServiceDomain, AWContext>>(provider => serviceDomain =>
            {
                string connectionString = string.Empty;
                Assembly configurationsAssembly;

                var configuration = provider.GetRequiredService<IConfiguration>();

                if (serviceDomain == ServiceDomain.Customer)
                {
                    connectionString = configuration["ConnectionStrings:CustomerDb"];
                    configurationsAssembly = typeof(CustomerConfiguration).Assembly;
                }
                else if (serviceDomain == ServiceDomain.Product)
                {
                    connectionString = configuration["ConnectionStrings:ProductDb"];
                    configurationsAssembly = typeof(ProductConfiguration).Assembly;
                }
                else
                    throw new ArgumentException($"ServiceDomain '{serviceDomain}' was not expected");

                var builder = new DbContextOptionsBuilder<AWContext>();
                builder.UseSqlServer(connectionString);
                builder.AddInterceptors(new AzureAdAuthenticationDbConnectionInterceptor());

                return new AWContext(
                    builder.Options,
                    provider.GetService<IMediator>(),
                    configurationsAssembly
                );
            });

            return services;
        }

        public static IServiceCollection AddCustomerServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Services.Customer.Core.Entities.Customer>>(provider =>
             {
                 var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                 var dbContext = func(ServiceDomain.Customer);
                 return new EfRepository<Services.Customer.Core.Entities.Customer>(dbContext);
             })
            .AddScoped<IRepository<Services.Customer.Core.Entities.StoreCustomer>>(provider =>
            {
                var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                var dbContext = func(ServiceDomain.Customer);
                return new EfRepository<Services.Customer.Core.Entities.StoreCustomer>(dbContext);
            })
            .AddScoped<IRepository<Services.Customer.Core.Entities.IndividualCustomer>>(provider =>
            {
                var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                var dbContext = func(ServiceDomain.Customer);
                return new EfRepository<Services.Customer.Core.Entities.IndividualCustomer>(dbContext);
            })
            .AddScoped<IRepository<Services.Customer.Core.Entities.Address>>(provider =>
             {
                 var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                 var dbContext = func(ServiceDomain.Customer);
                 return new EfRepository<Services.Customer.Core.Entities.Address>(dbContext);
             });

            return services;
        }

        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Services.Product.Core.Entities.Product>>(provider =>
             {
                 var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                 var dbContext = func(ServiceDomain.Product);
                 return new EfRepository<Services.Product.Core.Entities.Product>(dbContext);
             })
            .AddScoped<IRepository<Services.Product.Core.Entities.ProductCategory>>(provider =>
            {
                var func = provider.GetRequiredService<Func<ServiceDomain, AWContext>>();
                var dbContext = func(ServiceDomain.Product);
                return new EfRepository<Services.Product.Core.Entities.ProductCategory>(dbContext);
            });

            return services;
        }

        public static IServiceCollection AddIdentityServerServices(this IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer(configuration["ConnectionStrings:IdentityServerDb"]);
                builder.AddInterceptors(new AzureAdAuthenticationDbConnectionInterceptor());

                return new ApplicationDbContext(builder.Options);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}