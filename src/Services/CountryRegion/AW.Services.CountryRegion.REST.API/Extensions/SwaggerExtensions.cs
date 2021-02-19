using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AW.Services.CountryRegion.REST.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string virtualPath,
            IApiVersionDescriptionProvider provider)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{virtualPath}/swagger/{description.GroupName}/swagger.json", $"Country API {description.GroupName.ToUpperInvariant()}");
                        options.RoutePrefix = string.Empty;
                    }
                    options.DocumentTitle = "Country API Documentation";
                });

            return app;
        }
    }
}