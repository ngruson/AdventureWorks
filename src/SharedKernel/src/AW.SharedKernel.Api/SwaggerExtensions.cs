using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.SharedKernel.Api
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string apiName)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>(s =>
            {
                var config = s.GetRequiredService<IConfiguration>();
                var provider = s.GetRequiredService<IApiVersionDescriptionProvider>();
                var logger = s.GetRequiredService<ILogger<ConfigureSwaggerOptions>>();
                return new ConfigureSwaggerOptions(logger, config, provider, apiName);
            });
            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, 
            string virtualPath,
            IConfiguration config,
            IApiVersionDescriptionProvider provider,
            string apiName)
        {
            var clientId = config.GetValue<string>("AuthN:SwaggerClientId");
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var groupName in provider.ApiVersionDescriptions.Select(_ => _.GroupName))
                    {
                        options.SwaggerEndpoint($"{virtualPath}/swagger/{groupName}/swagger.json", $"{apiName} {groupName.ToUpperInvariant()}");
                        options.RoutePrefix = string.Empty;
                    }
                    options.DocumentTitle = $"{apiName} Documentation";
                    options.OAuthClientId(clientId);
                    options.OAuthAppName("AdventureWorks");
                    options.OAuthUsePkce();
                });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration config;
        private readonly IApiVersionDescriptionProvider provider;
        private readonly string apiName;
        private readonly ILogger<ConfigureSwaggerOptions> logger;

        public ConfigureSwaggerOptions(ILogger<ConfigureSwaggerOptions> logger, IConfiguration config, IApiVersionDescriptionProvider provider, string apiName)
        {
            this.config = config;
            this.provider = provider;
            this.apiName = apiName;
            this.logger = logger;
        }

        public void Configure(SwaggerGenOptions options)
        {
            var disco = GetDiscoveryDocument().GetAwaiter().GetResult();
            
            var apiScope = config.GetValue<string>("AuthN:ApiName");
            var scopes = apiScope.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var additionalScopes = config.GetValue<string>("AuthN:AdditionalScopes");
            scopes.AddRange(additionalScopes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList());

            var oauthScopeDic = new Dictionary<string, string>();
            foreach (var scope in scopes)
            {
                oauthScopeDic.Add(scope, $"Resource access: {scope}");
            }
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = $"{apiName} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    });
            }
            options.EnableAnnotations();
            options.CustomSchemaIds(x => x.FullName);
            options.DescribeAllParametersInCamelCase();

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(disco.AuthorizeEndpoint),
                        TokenUrl = new Uri(disco.TokenEndpoint),
                        Scopes = oauthScopeDic
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                    },
                    oauthScopeDic.Keys.ToArray()
                }
            });
        }
        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocument()
        {
            var client = new HttpClient();
            var authority = config.GetValue<string>("AuthN:Authority");
            return await client.GetDiscoveryDocumentAsync(authority);
        }
    }
}