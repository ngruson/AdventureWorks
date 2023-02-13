using AW.SharedKernel.Interfaces;
using AW.SharedKernel.OpenIdConnect;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.Store;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IdentityModel.Tokens.Jwt;

static class CustomExtensionMethods
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
        });

        return services;
    }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddScoped<IApplication, Application>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, OpenIdConnectConfiguration oidcConfig)
    {
        if (oidcConfig.IdentityProvider == IdentityProvider.IdentityServer)
            services.AddOpenIdConnectAccessTokenManagement();

        services.AddHttpClient<IBasketApiClient, BasketApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BasketAPI:Uri"]!);
        })
        .AddUserAccessTokenHandler();

        services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CustomerAPI:Uri"]!);
        })
        .AddUserAccessTokenHandler();

        services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ProductAPI:Uri"]!);
        })
        .AddClientAccessTokenHandler();

        services.AddHttpClient<IReferenceDataApiClient, ReferenceDataApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ReferenceDataAPI:Uri"]!);
        })
        .AddUserAccessTokenHandler();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "cookie";
            options.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("cookie")
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = configuration["AuthN:Authority"];
            options.ClientId = configuration["AuthN:ClientId"];
            options.ClientSecret = configuration["AuthN:ClientSecret"];
            options.ResponseType = "code";
            options.UsePkce = true;
            options.SaveTokens = true;
            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.Scope.Add("offline_access");
            options.Scope.Add("basket-api.read");
            options.Scope.Add("basket-api.write");
            options.Scope.Add("customer-api.read");
            options.Scope.Add("product-api.read");
            options.Scope.Add("referencedata-api.read");
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapUniqueJsonKey("customer_number", "customer_number");
        });

        return services;
    }

    public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
        hcBuilder.AddElasticsearch(configuration["ElasticSearchUri"]!);
        hcBuilder.AddIdentityServer(new Uri(configuration["AuthN:Authority"]!));
        hcBuilder.AddUrlGroup(new Uri(configuration["BasketAPI:Uri"]!), name: "basket-api");
        hcBuilder.AddUrlGroup(new Uri(configuration["ProductAPI:Uri"]!), name: "product-api");

        return services;
    }
}