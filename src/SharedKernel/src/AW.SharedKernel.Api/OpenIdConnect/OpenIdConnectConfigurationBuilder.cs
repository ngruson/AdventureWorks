﻿using Microsoft.Extensions.Configuration;

namespace AW.SharedKernel.Api.OpenIdConnect
{
    public class OpenIdConnectConfigurationBuilder
    {
        private readonly IConfiguration _configuration;

        public OpenIdConnectConfigurationBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public OpenIdConnectConfiguration Build()
        {
            if (_configuration["AuthN:IdP"] == "AzureAd")
            {
                return new OpenIdConnectConfiguration
                {
                    IdentityProvider = IdentityProvider.AzureAd,
                    Authority = _configuration["AuthN:AzureAd:Instance"],
                    WellKnownEndpoint = $"https://login.microsoftonline.com/{_configuration["AuthN:AzureAd:TenantId"]}/v2.0/.well-known/openid-configuration",
                    ClientId = _configuration["AuthN:AzureAd:ClientId"],
                    Scopes = _configuration["AuthN:AzureAd:Scopes"],
                    AdditionalScopes = _configuration["AuthN:AzureAd:AdditionalScopes"]
                };
            }
            else if (_configuration["AuthN:IdP"] == "IdSrv")
            {
                return new OpenIdConnectConfiguration
                {
                    IdentityProvider = IdentityProvider.IdentityServer,
                    Authority = _configuration["AuthN:IdSrv:Authority"],
                    WellKnownEndpoint = _configuration["AuthN:IdSrv:Authority"],
                    ClientId = _configuration["AuthN:IdSrv:ClientId"],
                    Scopes = _configuration["AuthN:IdSrv:Scopes"],
                    AdditionalScopes = _configuration["AuthN:IdSrv:AdditionalScopes"]
                };
            }

            return null;
        }
    }
}