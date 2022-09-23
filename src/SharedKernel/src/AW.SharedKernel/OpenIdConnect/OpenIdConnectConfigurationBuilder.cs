using Microsoft.Extensions.Configuration;

namespace AW.SharedKernel.OpenIdConnect
{
    public class OpenIdConnectConfigurationBuilder
    {
        private readonly IConfiguration _configuration;

        public OpenIdConnectConfigurationBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public OpenIdConnectConfiguration? Build()
        {
            if (_configuration["AuthN:IdP"] == "AzureAd")
            {
                return new OpenIdConnectConfiguration(
                    identityProvider: IdentityProvider.AzureAd,
                    authority: _configuration["AuthN:AzureAd:Instance"],
                    wellKnownEndpoint: $"https://login.microsoftonline.com/{_configuration["AuthN:AzureAd:TenantId"]}/v2.0/.well-known/openid-configuration",
                    clientId: _configuration["AuthN:AzureAd:ClientId"],
                    clientSecret: _configuration["AuthN:AzureAd:ClientSecret"],
                    scopes: _configuration["AuthN:Scopes"],
                    openIdClientId: _configuration["AuthN:AzureAd:OpenIdClientId"]
                );
            }
            else if (_configuration["AuthN:IdP"] == "IdSrv")
            {
                return new OpenIdConnectConfiguration(
                    identityProvider: IdentityProvider.IdentityServer,
                    authority: _configuration["AuthN:IdSrv:Authority"],
                    wellKnownEndpoint: _configuration["AuthN:IdSrv:Authority"],
                    clientId: _configuration["AuthN:IdSrv:ClientId"],
                    clientSecret: _configuration["AuthN:IdSrv:ClientSecret"],
                    scopes: _configuration["AuthN:Scopes"],
                    openIdClientId: _configuration["AuthN:IdSrv:OpenIdClientId"]
                );
            }

            return null;
        }
    }
}