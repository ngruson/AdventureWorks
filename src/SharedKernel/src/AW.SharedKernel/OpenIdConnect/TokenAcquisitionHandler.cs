using Microsoft.Identity.Web;
using System.Net.Http.Headers;

namespace AW.SharedKernel.OpenIdConnect
{
    public class TokenAcquisitionHandler : DelegatingHandler
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string[] _scopes;

        public TokenAcquisitionHandler(ITokenAcquisition tokenAcquisition, string[] scopes)
        {
            _tokenAcquisition = tokenAcquisition;
            _scopes = scopes;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(_scopes);

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
