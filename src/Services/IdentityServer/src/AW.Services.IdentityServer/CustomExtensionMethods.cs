using AW.Services.IdentityServer.Core.Data;
using AW.Services.IdentityServer.Core.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Cryptography.X509Certificates;

namespace AW.Services.IdentityServer
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(opt => opt.EnableEndpointRouting = false);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.RequireHeaderSymmetry = false;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            services.AddRazorPages();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddSigningCredential(new X509Certificate2("identityserver.pfx"))
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        configuration.GetConnectionString("DbConnection"),
                        options => options.MigrationsAssembly(migrationsAssembly)
                    );
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        configuration.GetConnectionString("DbConnection"),
                        options => options.MigrationsAssembly(migrationsAssembly)
                    );
                });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection")!);
            hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);

            return services;
        }
    }
}