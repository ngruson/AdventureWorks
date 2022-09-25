using AW.Services.IdentityServer;
using AW.Services.IdentityServer.Configuration;
using AW.Services.IdentityServer.Core.Data;
using AW.Services.IdentityServer.Core.Models;
using Duende.IdentityServer.EntityFramework.DbContexts;
using HealthChecks.UI.Client;
using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace AW.Services.IdentityServer
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
            services
                .AddCustomMvc()
                .AddDatabase(Configuration)
                .AddIdentity(Configuration)
                .AddCustomHealthCheck(Configuration);

            //.AddInMemoryClients(InMemoryConfiguration.Clients())
            //.AddInMemoryApiResources(InMemoryConfiguration.ApiResources())
            //.AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes())
            //.AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var virtualPath = "/identityserver";
            MigrateInMemoryDataToSqlServer(app);

            app.Map(virtualPath, builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                }

                builder.UseForwardedHeaders();

                builder.Use(async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    await next();
                });

                builder.UseStaticFiles();
                builder.UseRouting();
                builder.UseIdentityServer();
                builder.UseAuthorization();
                builder.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                    {
                        Predicate = r => r.Name.Contains("self")
                    });
                });
            });
        }

        public void MigrateInMemoryDataToSqlServer(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in InMemoryConfiguration.Clients())
                {
                    context.Clients.Add(client);
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in InMemoryConfiguration.IdentityResources())
                {
                    context.IdentityResources.Add(resource);
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in InMemoryConfiguration.ApiResources())
                {
                    context.ApiResources.Add(resource);
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in InMemoryConfiguration.ApiScopes())
                {
                    context.ApiScopes.Add(apiScope);
                }
                context.SaveChanges();
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            foreach (var user in InMemoryConfiguration.Users(Configuration))
            {
                var appUser = userManager.FindByNameAsync(user.Username).Result;
                if (appUser == null)
                {
                    appUser = new ApplicationUser
                    {
                        UserName = user.Username,
                        Email = user.Claims.Single(c => c.Type == "email").Value,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(appUser, user.Password).Result;
                    result = userManager.AddClaimsAsync(appUser, new Claim[]{
                            new Claim(JwtClaimTypes.Name, Configuration["TestUser:Claims:Name"]),
                            new Claim(JwtClaimTypes.GivenName, Configuration["TestUser:Claims:GivenName"]),
                            new Claim(JwtClaimTypes.FamilyName, Configuration["TestUser:Claims:FamilyName"])
                        }).Result;
                };
            }
        }
    }
}

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
        hcBuilder.AddSqlServer(configuration.GetConnectionString("DbConnection"));
        hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]);

        return services;
    }
}