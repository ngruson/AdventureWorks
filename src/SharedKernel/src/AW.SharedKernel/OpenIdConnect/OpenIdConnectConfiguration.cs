namespace AW.SharedKernel.OpenIdConnect
{
    public class OpenIdConnectConfiguration
    {
        public OpenIdConnectConfiguration(
            IdentityProvider identityProvider,
            string authority,
            string wellKnownEndpoint,
            string clientId,
            string clientSecret,
            string scopes,
            string openIdClientId
        )
        {
            IdentityProvider = identityProvider;
            Authority = authority;
            WellKnownEndpoint = wellKnownEndpoint;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scopes = scopes;
            OpenIdClientId = openIdClientId;
        }

        public IdentityProvider IdentityProvider { get; init; }
        public string Authority { get; init; }
        public string WellKnownEndpoint { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string Scopes { get; init; }
        public string OpenIdClientId { get; init; }
    }
}