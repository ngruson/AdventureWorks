namespace AW.SharedKernel.OpenIdConnect
{
    public class OpenIdConnectConfiguration
    {
        public IdentityProvider IdentityProvider { get; init; }
        public string Authority { get; init; }
        public string WellKnownEndpoint { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string[] Scopes { get; init; }
        public string OpenIdClientId { get; init; }
    }
}