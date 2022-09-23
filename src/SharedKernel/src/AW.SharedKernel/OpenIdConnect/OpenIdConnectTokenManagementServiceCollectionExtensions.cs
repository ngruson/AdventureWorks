using Duende.AccessTokenManagement.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace AW.SharedKernel.OpenIdConnect
{
    public static class OpenIdConnectTokenManagementServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddUserAccessTokenHandler(
            this IHttpClientBuilder httpClientBuilder,
            IdentityProvider identityProvider, string[] scopes,
            UserTokenRequestParameters? parameters = null)
        {
            if (identityProvider == IdentityProvider.AzureAd)
                return httpClientBuilder.AddHttpMessageHandler(provider =>
                {
                    var tokenAcquisition = provider.GetRequiredService<ITokenAcquisition>();
                    return new TokenAcquisitionHandler(tokenAcquisition, scopes);
                });
            else
                return httpClientBuilder.AddHttpMessageHandler(provider =>
                {
                    var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

                    return new OpenIdConnectUserAccessTokenHandler(contextAccessor, parameters);
                });
        }
    }
}