namespace AW.SharedKernel.Api.OpenIdConnect
{
    public class OpenIdConnectConfiguration
    {
        public IdentityProvider IdentityProvider { get; init; }
        public string Authority { get; init; }
        public string WellKnownEndpoint { get; init; }
        public string ClientId { get; init; }
        public string Scopes { get; init; }
        public string AdditionalScopes { get; init; }
        public string OpenIdClientId { get; init; }
    }
}