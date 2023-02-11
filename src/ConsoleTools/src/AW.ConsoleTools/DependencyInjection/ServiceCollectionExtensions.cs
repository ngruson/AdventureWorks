using AW.Services.SharedKernel.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AW.Services.Customer.Infrastructure.EFCore.Configurations;
using Microsoft.Extensions.Configuration;
using AW.Services.IdentityServer.Core.Data;
using AW.Services.IdentityServer.Core.Models;
using Microsoft.AspNetCore.Identity;
using AW.Services.Product.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.HumanResources.Infrastructure.EFCore.Configurations;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Extensions.Logging;

namespace AW.ConsoleTools.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddRepositoryFactory<T>(this IServiceCollection services)
            where T : class, IAggregateRoot
        {
            services.AddSingleton<IRepositoryFactory<T>, RepositoryFactory<T>>();

            return services;
        }

        public static IServiceCollection AddCustomerServices(this IServiceCollection services)
        {
            services
                .AddRepositoryFactory<Services.Customer.Core.Entities.Customer>()
                .AddRepositoryFactory<Services.Customer.Core.Entities.IndividualCustomer>()
                .AddRepositoryFactory<Services.Customer.Core.Entities.StoreCustomer>()
                .AddRepositoryFactory<Services.Customer.Core.Entities.Address>()
                .AddScoped(provider =>
                {
                    return CreateRepository<Services.Customer.Core.Entities.Customer>(provider);
                })
                .AddScoped(provider =>
                {
                    return CreateRepository<Services.Customer.Core.Entities.IndividualCustomer>(provider);
                })
                .AddScoped(provider =>
                {
                    return CreateRepository<Services.Customer.Core.Entities.StoreCustomer>(provider);
                })
                .AddScoped(provider =>
                {
                    return CreateRepository<Services.Customer.Core.Entities.Address>(provider);
                });

            static IRepository<T> CreateRepository<T>(IServiceProvider provider)
                where T : class, IAggregateRoot
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var factory = provider.GetRequiredService<IRepositoryFactory<T>>();

                return factory.Create(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    provider.GetRequiredService<IAWContextFactory>(),
                    configuration["ConnectionStrings:CustomerDb"]!,
                    provider.GetRequiredService<IMediator>(),
                    typeof(CustomerConfiguration).Assembly
                );
            }

            return services;
        }

        public static IServiceCollection AddHumanResourcesServices(this IServiceCollection services)
        {
            services.AddRepositoryFactory<Services.HumanResources.Core.Entities.Employee>();

            services.AddScoped(provider =>
            {
                return CreateRepository<Services.HumanResources.Core.Entities.Employee>(provider);
            });

            static IRepository<T> CreateRepository<T>(IServiceProvider provider)
                where T : class, IAggregateRoot
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var factory = provider.GetRequiredService<IRepositoryFactory<T>>();

                return factory.Create(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    provider.GetRequiredService<IAWContextFactory>(),
                    configuration["ConnectionStrings:HumanResourcesDb"]!,
                    provider.GetRequiredService<IMediator>(),
                    typeof(EmployeeConfiguration).Assembly
                );
            }

            return services;
        }

        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddRepositoryFactory<Services.Product.Core.Entities.Product>();

            services.AddScoped(provider =>
            {
                return CreateRepository<Services.Product.Core.Entities.Product>(provider);
            })
            .AddScoped(provider =>
            {
                return CreateRepository<Services.Product.Core.Entities.ProductCategory>(provider);
            });

            static IRepository<T> CreateRepository<T>(IServiceProvider provider)
                where T : class, IAggregateRoot
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var factory = provider.GetRequiredService<IRepositoryFactory<T>>();

                return factory.Create(
                    provider.GetRequiredService<ILogger<AWContext>>(),
                    provider.GetRequiredService<IAWContextFactory>(),
                    configuration["ConnectionStrings:ProductDb"]!,
                    provider.GetRequiredService<IMediator>(),
                    typeof(ProductConfiguration).Assembly
                );
            }

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

        public static IServiceCollection AddGraphClient(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var options = new TokenCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };

                var clientSecretCredential = new ClientSecretCredential(
                    configuration["Graph:TenantId"],
                    configuration["Graph:ClientId"],
                    configuration["Graph:ClientSecret"], 
                    options
                );

                return new GraphServiceClient(clientSecretCredential, scopes);
            });

            return services;
        }
    }
}