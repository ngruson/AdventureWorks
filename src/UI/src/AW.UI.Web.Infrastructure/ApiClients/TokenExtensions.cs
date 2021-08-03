using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public static class TokenExtensions
    {
        public static async Task HandleToken(this HttpClient client, string authority, string clientId, string secret, string scope, string refreshToken)
        {
            //client.getac
            var accessToken = await client.GetRefreshTokenAsync(authority, clientId, secret, scope, refreshToken);
            client.SetBearerToken(accessToken);
        }

        private static async Task<string> GetRefreshTokenAsync(this HttpClient client, string authority, string clientId, string secret, string scope, string refreshToken)
        {
            var disco = await client.GetDiscoveryDocumentAsync(authority);
            if (disco.IsError) throw new Exception(disco.Error);

            await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = secret,
                Scope = scope,
                RefreshToken = refreshToken
            });
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = secret,
                Scope = scope
            });

            if (!tokenResponse.IsError) return tokenResponse.AccessToken;
            return null;
        }
    }
}